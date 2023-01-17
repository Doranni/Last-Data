using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Firing : MonoBehaviour
{
    [SerializeField] private float laserPower, laserLengthRange, laserAngleRande;
    [SerializeField] private GameObject laserLeft, laserRight;
    [SerializeField] private LayerMask lMask_meteorites;

    private LineRenderer laserLeft_lineRenderer, laserRight_lineRenderer;
    private List<Collider> targetsLeftAvailable = new(), targetsRightAvailable = new();
    private (bool isSet, MeteoriteController meteoriteController, Collider collider) targetLeft = (false, null, null), 
        targetRight = (false, null, null);
    private bool isLaserLeftActive = false, isLaserRightActive = false;

    public (bool isSet, MeteoriteController meteoriteController, Collider collider) TargetLeft => targetLeft;
    public (bool isSet, MeteoriteController meteoriteController, Collider collider) TargetRight => targetRight;

    [SerializeField] private AudioClip audioClip_laserBeamStart, audioClip_laserBeamMiddle, audioClip_laserBeamEnd;

    private AudioSource audioSource;
    private Coroutine audioCoroutineLeftLaser, audioCoroutineRightLaser;

    private enum Laser
    {
        left,
        right
    }


    private void Awake()
    {
        laserLeft_lineRenderer = laserLeft.GetComponentInChildren<LineRenderer>();
        laserRight_lineRenderer = laserRight.GetComponentInChildren<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        InputManager.Instance.OnFireLeft_started += _ => FireLeft_Started();
        InputManager.Instance.OnFireLeft_canceled += _ => FireLeft_Canceled();
        InputManager.Instance.OnFireRight_started += _ => FireRight_Started();
        InputManager.Instance.OnFireRight_canceled += _ => FireRight_Canceled();

        laserLeft.SetActive(false);
        laserRight.SetActive(false);

        audioSource.clip = audioClip_laserBeamMiddle;
        audioSource.loop = true;
    }

    private IEnumerator PlayAudioStart()
    {
        audioSource.PlayOneShot(audioClip_laserBeamStart);
        yield return new WaitForSeconds(audioClip_laserBeamStart.length);
        audioSource.Play();
    }

    private void PlayAudioEnd()
    {
        if (!isLaserLeftActive && !isLaserRightActive)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(audioClip_laserBeamEnd);
    }


    private void FireRight_Started()
    {
        if (ShipStorage.Instance.HasPower())
        {
            laserRight.SetActive(true);
            isLaserRightActive = true;
            audioCoroutineRightLaser = StartCoroutine(PlayAudioStart());
        }
    }

    private void FireRight_Canceled()
    {
        if (isLaserRightActive)
        {
            isLaserRightActive = false;
            laserRight.SetActive(false);
            ReleaseTarget(Laser.right);
            targetsRightAvailable.Clear();
            SetLaserPoints(Laser.right, true);
            PlayAudioEnd();
            StopCoroutine(audioCoroutineRightLaser);
        }    
    }

    private void FireLeft_Started()
    {
        if (ShipStorage.Instance.HasPower())
        {
            laserLeft.SetActive(true);
            isLaserLeftActive = true;
            audioCoroutineLeftLaser = StartCoroutine(PlayAudioStart());
        }
    }

    private void FireLeft_Canceled()
    {
        if (isLaserLeftActive)
        {
            isLaserLeftActive = false;
            laserLeft.SetActive(false);
            ReleaseTarget(Laser.left);
            targetsLeftAvailable.Clear();
            SetLaserPoints(Laser.left, true);
            PlayAudioEnd();
            StopCoroutine(audioCoroutineLeftLaser);
        }
    }

    private void SetTarget(Collider target, Laser laser)
    {
        switch (laser)
        {
            case Laser.left:
                {
                    targetLeft = (true, target.GetComponent<MeteoriteController>(), target);
                    targetLeft.meteoriteController.LaserTargetSet();
                    break;
                }
            default:
                {
                    targetRight = (true, target.GetComponent<MeteoriteController>(), target);
                    targetRight.meteoriteController.LaserTargetSet();
                    break;
                }
        }
    }

    private void ReleaseTarget(Laser laser)
    {
        switch (laser)
        {
            case Laser.left:
                {
                    if (targetLeft.meteoriteController != null)
                    {
                        targetLeft.meteoriteController.LaserTargetRelease();
                    }
                    targetLeft = (false, null, null);
                    break;
                }
            default:
                {
                    if (targetRight.meteoriteController != null)
                    {
                        targetRight.meteoriteController.LaserTargetRelease();
                    }
                    targetRight = (false, null, null);
                    break;
                }
        }
    }

    private void LaserHit(Laser laser)
    {
        SetLaserPoints(laser);
        switch (laser)
        {
            case Laser.left:
                {
                    if (targetLeft.isSet)
                    {
                        targetLeft.meteoriteController.ChangeHealth(-laserPower * Time.deltaTime);
                    }
                    break;
                }
            default:
                {
                    if (targetRight.isSet)
                    {
                        targetRight.meteoriteController.ChangeHealth(-laserPower * Time.deltaTime);
                    }
                    break;
                }
        }
        ShipStorage.Instance.PowerLaser();
    }

    private void SetLaserPoints(Laser laser, bool toReset = false)
    {
        LineRenderer lineRenderer;
        Transform laserTransform;
        Collider target;
        switch (laser)
        {
            case Laser.left:
                {
                    lineRenderer = laserLeft_lineRenderer;
                    laserTransform = laserLeft.transform;
                    target = targetLeft.collider;
                    break;
                }
            default:
                {
                    lineRenderer = laserRight_lineRenderer;
                    laserTransform = laserRight.transform;
                    target = targetRight.collider;
                    break;
                }
        }
        if (toReset || target == null || !target.enabled)
        {
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.forward * 30, Vector3.forward * 70,
                Vector3.forward * 100, });
        }
        else
        {
            Vector3 closestPoint = target.ClosestPoint(new Vector3(laserTransform.position.x,
                laserTransform.position.y, target.transform.position.z));
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero,
                laserTransform.InverseTransformPoint(Vector3.Lerp(laserTransform.position, closestPoint, 0.3f)),
                laserTransform.InverseTransformPoint(Vector3.Lerp(laserTransform.position, closestPoint, 0.7f)),
                laserTransform.InverseTransformPoint(closestPoint) });
        }
    }

    private void GetAvailableTargets(Laser laser)
    {
        Transform laserTransform;
        List<Collider> targets;
        switch (laser)
        {
            case Laser.left:
                {
                    laserTransform = laserLeft.transform;
                    targets = targetsLeftAvailable;
                    break;
                }
            default:
                {
                    laserTransform = laserRight.transform;
                    targets = targetsRightAvailable;
                    break;
                }
        }
        targets.Clear();
        Collider[] overlap = Physics.OverlapSphere(laserTransform.position, laserLengthRange, lMask_meteorites);
        for (int i = 0; i < overlap.Length; i++)
        {
            Vector3 closestPoint = overlap[i].ClosestPoint(new Vector3(laserTransform.position.x,
                laserTransform.position.y, overlap[i].transform.position.z));
            Vector3 heading = (closestPoint - laserTransform.position).normalized;
            float angle = Vector3.Angle(laserTransform.forward, heading);
            float distance = Vector3.Distance(closestPoint, laserTransform.position); 
            if (angle <= laserAngleRande && distance < laserLengthRange)
            {
                targets.Add(overlap[i]);
            }
        }
    }

    private bool ChooseMainTarget(Laser laser)
    {
        List<Collider> targets;
        Collider target;
        switch (laser)
        {
            case Laser.left:
                {
                    if (targetsLeftAvailable.Count == 0)
                    {
                        targetLeft = (false, null, null);
                        return false;
                    }
                    targets = targetsLeftAvailable;
                    break;
                }
            default:
                {
                    if (targetsRightAvailable.Count == 0)
                    {
                        targetRight = (false, null, null);
                        return false;
                    }
                    targets = targetsRightAvailable;
                    break;
                }
        }
        target = targets[0];
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].transform.position.z < target.transform.position.z)
            {
                target = targets[i];
            }
        }
        SetTarget(target, laser);
        return true;
    }

    private bool IsTargetValid(Laser laser)
    {
        Transform laserTransform;
        Collider targetCollider;
        MeteoriteController targetMeteoriteController;
        switch (laser)
        {
            case Laser.left:
                {
                    laserTransform = laserLeft.transform;
                    targetCollider = targetLeft.collider;
                    targetMeteoriteController = targetLeft.meteoriteController;
                    break;
                }
            default:
                {
                    laserTransform = laserRight.transform;
                    targetCollider = targetRight.collider;
                    targetMeteoriteController = targetRight.meteoriteController;
                    break;
                }
        }
        if (targetCollider == null || targetMeteoriteController.IsDead())
        {
            ReleaseTarget(laser);
            return false;
        }
        Vector3 closestPoint = targetCollider.GetComponent<Collider>().ClosestPoint(new Vector3(laserTransform.position.x,
                laserTransform.position.y, targetCollider.transform.position.z));
        Vector3 heading = (closestPoint - laserTransform.position).normalized;
        float angle = Vector3.Angle(laserTransform.forward, heading);
        if (angle > laserAngleRande)
        {
            ReleaseTarget(laser);
            return false;
        }
        else
        {
            return true;
        }
    }

    private void FixedUpdate()
    {
        if (isLaserLeftActive)
        {
            if (ShipStorage.Instance.HasPower())
            {
                if (!IsTargetValid(Laser.left))
                {
                    GetAvailableTargets(Laser.left);
                    ChooseMainTarget(Laser.left);
                }
                LaserHit(Laser.left);
            }
            else
            {
                FireLeft_Canceled();
            }
        }
        if (isLaserRightActive)
        {
            if (ShipStorage.Instance.HasPower())
            {
                if (!IsTargetValid(Laser.right))
                {
                    GetAvailableTargets(Laser.right);
                    ChooseMainTarget(Laser.right);
                }
                LaserHit(Laser.right);
            }
            else
            {
                FireRight_Canceled();
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnFireLeft_started -= _ => FireLeft_Started();
        InputManager.Instance.OnFireLeft_canceled -= _ => FireLeft_Canceled();
        InputManager.Instance.OnFireRight_started -= _ => FireRight_Started();
        InputManager.Instance.OnFireRight_canceled -= _ => FireRight_Canceled();
    }
}
