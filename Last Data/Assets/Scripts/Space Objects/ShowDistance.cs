using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ShowDistance : MonoBehaviour
{
    [Serializable]
    private enum DistanceType
    {
        undefined,
        unreachable,
        far, 
        mid,
        close,
        behind,
        interaction
    }

    [SerializeField] private float distanceUnreachable, distanceFar, distanceMid, distanceClose;
    [SerializeField] private Color colorFar, colorMid, colorClose, colorInteracted;
        private Color colorBehind = new Color(0, 0, 0, 0);

    private new Collider collider;

    private Outline outline;

    [SerializeField] private DistanceType currentDistanceType;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    { 
        currentDistanceType = DistanceType.undefined;
        outline.OutlineColor = colorBehind;
        CheckDistance();
    }

    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (currentDistanceType != DistanceType.interaction)
        {
            Vector3 point = collider.ClosestPoint(new Vector3(0, 0, transform.position.z));
            if (point.x > GameManager.Instance.ObjDetectRange_min.x && point.x < GameManager.Instance.ObjDetectRange_max.x &&
                point.y > GameManager.Instance.ObjDetectRange_min.y && point.y < GameManager.Instance.ObjDetectRange_max.y)
            {
                if (point.z > distanceUnreachable && currentDistanceType != DistanceType.unreachable)
                {
                    currentDistanceType = DistanceType.unreachable;
                    outline.OutlineColor = colorBehind;
                }
                else if (point.z > distanceFar && point.z < distanceUnreachable && currentDistanceType != DistanceType.far)
                {
                    currentDistanceType = DistanceType.far;
                    outline.OutlineColor = colorFar;
                }
                else if (point.z > distanceMid && point.z < distanceFar && currentDistanceType != DistanceType.mid)
                {
                    currentDistanceType = DistanceType.mid;
                    outline.OutlineColor = colorMid;
                }
                else if (point.z > distanceClose && point.z < distanceMid && currentDistanceType != DistanceType.close)
                {
                    currentDistanceType = DistanceType.close;
                    outline.OutlineColor = colorClose;
                }
                else if (point.z < distanceClose && currentDistanceType != DistanceType.behind)
                {
                    currentDistanceType = DistanceType.behind;
                    outline.OutlineColor = colorBehind;
                }
            }
            else if (currentDistanceType != DistanceType.undefined)
            {
                ClearOutlining();
            }
        }
    }

    public void ClearOutlining()
    {
        currentDistanceType = DistanceType.undefined;
        outline.OutlineColor = colorBehind;
    }

    public void StartInteraction()
    {
        currentDistanceType = DistanceType.interaction;
        outline.OutlineColor = colorInteracted;
    }

    public void StopInteraction()
    {
        ClearOutlining();
    }
}
