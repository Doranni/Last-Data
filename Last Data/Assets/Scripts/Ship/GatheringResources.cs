using System.Collections.Generic;
using UnityEngine;

public class GatheringResources : MonoBehaviour
{
    [SerializeField] private float vacuumRange = 20, gatherRange = 6, vacuumStrength = 50;
    [SerializeField] private LayerMask lMask_resources;

    private List<(Rigidbody rb, Collider collider, Resource resource)> targets = new();

    private bool isActivated = false;

    private void Start()
    {
        InputManager.Instance.OnVacuum_started += _ => VacuumStart();
        InputManager.Instance.OnVacuum_canceled += _ => VacuumCancel();
    }

    private void VacuumStart()
    {
        isActivated = true;
    }

    private void VacuumCancel()
    {
        isActivated = false;
    }

    private void GetAvailableTargets()
    {
        CheckTargets();
        Collider[] overlap = Physics.OverlapSphere(transform.position, vacuumRange, lMask_resources);
        for (int i = 0; i < overlap.Length; i++)
        {
            if (overlap[i].TryGetComponent(out Resource resource) && !resource.IsUnderInteraction)
            {
                Vector3 closestPoint = overlap[i].ClosestPoint(transform.position);
                float distance = Vector3.Distance(closestPoint, transform.position);
                if (distance < vacuumRange)
                {
                    targets.Add((overlap[i].GetComponent<Rigidbody>(), overlap[i], resource));
                    resource.VacuumTargetSet();
                }
            }
        }
    }

    private void CheckTargets()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].collider == null || !targets[i].collider.gameObject.activeInHierarchy)
            {
                targets.RemoveAt(i);
                continue;
            }
            Vector3 closestPoint = targets[i].collider.ClosestPoint(transform.position);
            float distance = Vector3.Distance(closestPoint, transform.position);
            if (distance > vacuumRange)
            {
                targets[i].resource.VacuumTargetRelease();
                targets.RemoveAt(i);
            }
        }
    }

    private void VacuumTargets()
    {
        if (ShipStorage.Instance.HasPower())
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].rb.AddForce((transform.position - targets[i].rb.position) * vacuumStrength, ForceMode.Force);
            }
            ShipStorage.Instance.PowerVacuum();
        }
    }

    private void GatherTargets()
    {
        Collider[] overlap = Physics.OverlapSphere(transform.position, gatherRange, lMask_resources);
        for (int i = 0; i < overlap.Length; i++)
        {
            float distance = Vector3.Distance(overlap[i].transform.position, transform.position);
            if (distance < gatherRange)
            {
                Debug.Log($"Gathering {overlap[i].name}");
                ShipStorage.Instance.GatherResource(overlap[i].gameObject);
                Destroy(overlap[i].gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isActivated)
        {
            GetAvailableTargets();
            VacuumTargets();
        }
        GatherTargets();
    }
}
