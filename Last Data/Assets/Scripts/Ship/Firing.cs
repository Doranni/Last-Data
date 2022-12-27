using UnityEngine;
using System.Collections.Generic;

public class Firing : MonoBehaviour
{
    [SerializeField] private float laserPower, laserLengthRange, laserAngleRande;
    [SerializeField] private GameObject laserLeft, laserRight;
    [SerializeField] private LayerMask lMask_Asteroids;

    private LineRenderer laserLeft_lineRenderer, laserRight_lineRenderer;
    private List<Collider> targetsLeft = new(), targetsRight = new();
    private Collider targetLeft = null, targetRight = null;
    private bool isLaserLeftActive = false, isLaserRightActive = false;

    private enum Laser
    {
        left,
        right
    }


    void Start()
    {
        InputManager.Instance.OnFireLeft_started += _ => FireLeft_Started();
        InputManager.Instance.OnFireLeft_canceled += _ => FireLeft_Canceled();
        InputManager.Instance.OnFireRight_started += _ => FireRight_Started();
        InputManager.Instance.OnFireRight_canceled += _ => FireRight_Canceled();

        laserLeft_lineRenderer = laserLeft.GetComponentInChildren<LineRenderer>();
        laserRight_lineRenderer = laserRight.GetComponentInChildren<LineRenderer>();

        laserLeft.SetActive(false);
        laserRight.SetActive(false);
}

    private void FireRight_Started()
    {
        laserRight.SetActive(true);
        isLaserRightActive = true;
    }

    private void FireRight_Canceled()
    {
        isLaserRightActive = false;
        laserRight.SetActive(false);
        ClearTargets(Laser.right);
        SetLaserPoints(Laser.right, true);
    }

    private void FireLeft_Started()
    {
        laserLeft.SetActive(true);
        isLaserLeftActive = true;
    }

    private void FireLeft_Canceled()
    {
        isLaserLeftActive = false;
        laserLeft.SetActive(false);
        ClearTargets(Laser.left);
        SetLaserPoints(Laser.left, true);
    }

    private void LaserHit(Laser laser)
    {
        SetLaserPoints(laser);
        Health targetHealth;
        switch (laser)
        {
            case Laser.left:
                {
                    if (targetLeft == null)
                    {
                        return;
                    }
                    targetHealth = targetLeft.GetComponent<Health>();
                    break;
                }
            default:
                {
                    if (targetRight == null)
                    {
                        return;
                    }
                    targetHealth = targetRight.GetComponent<Health>();
                    break;
                }
        }
        targetHealth.ChangeHealth(-laserPower * Time.deltaTime);
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
                    target = targetLeft;
                    break;
                }
            default:
                {
                    lineRenderer = laserRight_lineRenderer;
                    laserTransform = laserRight.transform;
                    target = targetRight;
                    break;
                }
        }
        if (toReset || target == null)
        {
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.forward * 30, Vector3.forward * 70,
                Vector3.forward * 100, });
        }
        else
        {
            lineRenderer.SetPositions(new Vector3[] { Vector3.zero,
                laserTransform.InverseTransformPoint(Vector3.Lerp(laserTransform.position, target.transform.position, 0.3f)),
                laserTransform.InverseTransformPoint(Vector3.Lerp(laserTransform.position, target.transform.position, 0.7f)),
                laserTransform.InverseTransformPoint(target.transform.position) });
        }
    }

    private void GetTargets(Laser laser)
    {
        Transform laserTransform;
        List<Collider> targets;
        switch (laser)
        {
            case Laser.left:
                {
                    laserTransform = laserLeft.transform;
                    targets = targetsLeft;
                    break;
                }
            default:
                {
                    laserTransform = laserRight.transform;
                    targets = targetsRight;
                    break;
                }
        }
        targets.Clear();
        Collider[] overlap = Physics.OverlapSphere(laserTransform.position, laserLengthRange, lMask_Asteroids);  
        for (int i = 0; i < overlap.Length; i++)
        {
            Vector3 closestPoint = overlap[i].ClosestPoint(new Vector3(laserTransform.position.x,
                laserTransform.position.y, overlap[i].transform.position.z));
            Vector3 heading = (closestPoint - laserTransform.position).normalized;
            float angle = Vector3.Angle(laserTransform.forward, heading);
            if (angle <= laserAngleRande)
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
                    if (targetsLeft.Count == 0)
                    {
                        targetLeft = null;
                        return false;
                    }
                    targets = targetsLeft;
                    break;
                }
            default:
                {
                    if (targetsRight.Count == 0)
                    {
                        targetRight = null;
                        return false;
                    }
                    targets = targetsRight;
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
        switch (laser)
        {
            case Laser.left:
                {
                    targetLeft = target;
                    break;
                }
            default:
                {
                    targetRight = target;
                    break;
                }
        }
        return true;
    }

    private bool CheckIsTargetValid(Laser laser)
    {
        Transform laserTransform;
        Collider target;
        switch (laser)
        {
            case Laser.left:
                {
                    laserTransform = laserLeft.transform;
                    target = targetLeft;
                    break;
                }
            default:
                {
                    laserTransform = laserRight.transform;
                    target = targetRight;
                    break;
                }
        }
        if (target == null)
        {
            return false;
        }
        Vector3 closestPoint = target.ClosestPoint(new Vector3(laserTransform.position.x,
                laserTransform.position.y, target.transform.position.z));
        Vector3 heading = (closestPoint - laserTransform.position).normalized;
        float angle = Vector3.Angle(laserTransform.forward, heading);
        return angle <= laserAngleRande;
    }

    private void ClearTargets(Laser laser)
    {
        switch (laser)
        {
            case Laser.left:
                {
                    targetLeft = null;
                    targetsLeft.Clear();
                    return;
                }
            default:
                {
                    targetRight = null;
                    targetsRight.Clear();
                    return;
                }
        }
    }

    void Update()
    {
        //Debug.DrawLine(laserLeft.transform.position, laserLeft.transform.position + laserLeft.transform.forward * 100);
        //Debug.DrawLine(laserRight.transform.position, laserRight.transform.position + laserRight.transform.forward * 100);
        //if (targetsLeft != null && targetsLeft.Count > 0)
        //{
        //    for (int i = 0; i < targetsLeft.Count; i++)
        //    {
        //        Debug.DrawLine(laserLeft.transform.position, targetsLeft[i].transform.position, Color.green);
        //    }
        //}
        //if (targetsRight != null && targetsRight.Count > 0)
        //{
        //    for (int i = 0; i < targetsRight.Count; i++)
        //    {
        //        Debug.DrawLine(laserRight.transform.position, targetsRight[i].transform.position, Color.blue);
        //    }
        //}
        //if (targetLeft != null)
        //{
        //    Debug.DrawLine(targetLeft.transform.position, targetLeft.transform.position + Vector3.up * 5, Color.green);
        //}
        //if (targetRight != null)
        //{
        //    Debug.DrawLine(targetRight.transform.position, targetRight.transform.position + Vector3.up * 5, Color.blue);
        //}
    }

    private void FixedUpdate()
    {
        if (isLaserLeftActive)
        {
            if (!CheckIsTargetValid(Laser.left))
            {
                GetTargets(Laser.left);
                ChooseMainTarget(Laser.left);
            }
            LaserHit(Laser.left);
        }
        if (isLaserRightActive)
        {
            if (!CheckIsTargetValid(Laser.right))
            {
                GetTargets(Laser.right);
                ChooseMainTarget(Laser.right);
            }
            LaserHit(Laser.right);
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
