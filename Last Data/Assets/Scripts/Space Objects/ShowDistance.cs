using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public class ShowDistance : MonoBehaviour
{
    public enum ObjectType
    {
        meteorite,
        resource
    }

    [Serializable]
    private enum Status
    {
        undefined,
        unreachable,
        far, 
        mid,
        close,
        behind,
        interaction
    }

    [SerializeField] private ObjectType type;
    [SerializeField] private Status status;

    private float distanceUnreachable, distanceFar, distanceMid, distanceClose;
    private Color colorFar, colorMid, colorClose;

    private new Collider collider;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        switch (type)
        {
            case ObjectType.meteorite:
                {
                    distanceUnreachable = GameManager.Instance.DistanceUnreachable_meteorite;
                    distanceFar = GameManager.Instance.DistanceFar_meteorite;
                    distanceMid = GameManager.Instance.DistanceMid_meteorite;
                    distanceClose = GameManager.Instance.DistanceClose_meteorite;
                    colorFar = GameManager.Instance.ColorFar_meteorite;
                    colorMid = GameManager.Instance.ColorMid_meteorite;
                    colorClose = GameManager.Instance.ColorClose_meteorite;
                    break;
                }
            case ObjectType.resource:
                {
                    distanceUnreachable = GameManager.Instance.DistanceUnreachable_resource;
                    distanceFar = GameManager.Instance.DistanceFar_resource;
                    distanceMid = GameManager.Instance.DistanceMid_resource;
                    distanceClose = GameManager.Instance.DistanceClose_resource;
                    colorFar = GameManager.Instance.ColorFar_resource;
                    colorMid = GameManager.Instance.ColorMid_resource;
                    colorClose = GameManager.Instance.ColorClose_resource;
                    break;
                }
        }
        status = Status.undefined;
        outline.OutlineColor = GameManager.Instance.ColorTransparent;
        CheckDistance();
    }

    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (status != Status.interaction)
        {
            Vector3 point = collider.ClosestPoint(new Vector3(0, 0, transform.position.z));
            if (point.x > GameManager.Instance.ObjDetectRange_min.x && point.x < GameManager.Instance.ObjDetectRange_max.x &&
                point.y > GameManager.Instance.ObjDetectRange_min.y && point.y < GameManager.Instance.ObjDetectRange_max.y)
            {
                if (point.z > distanceUnreachable && status != Status.unreachable)
                {
                    status = Status.unreachable;
                    outline.OutlineColor = GameManager.Instance.ColorTransparent;
                }
                else if (point.z > distanceFar && point.z < distanceUnreachable && status != Status.far)
                {
                    status = Status.far;
                    outline.OutlineColor = colorFar;
                }
                else if (point.z > distanceMid && point.z < distanceFar && status != Status.mid)
                {
                    status = Status.mid;
                    outline.OutlineColor = colorMid;
                }
                else if (point.z > distanceClose && point.z < distanceMid && status != Status.close)
                {
                    status = Status.close;
                    outline.OutlineColor = colorClose;
                }
                else if (point.z < distanceClose && status != Status.behind)
                {
                    status = Status.behind;
                    outline.OutlineColor = GameManager.Instance.ColorTransparent;
                }
            }
            else if (status != Status.undefined)
            {
                ClearOutlining();
            }
        }
    }

    public void ClearOutlining()
    {
        status = Status.undefined;
        outline.OutlineColor = GameManager.Instance.ColorTransparent;
    }

    public void StartInteraction()
    {
        status = Status.interaction;
        outline.OutlineColor = GameManager.Instance.ColorInteracted;
    }

    public void StopInteraction()
    {
        ClearOutlining();
    }
}
