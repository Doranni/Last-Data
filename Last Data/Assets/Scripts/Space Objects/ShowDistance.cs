using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ShowDistance : MonoBehaviour
{
    [Serializable]
    private enum Distance
    {
        undefined,
        far, 
        mid,
        close,
        behind
    }

    [SerializeField] private float distanceFar, distanceMid, distanceClose;
    [SerializeField] private Color colorFar, colorMid, colorClose, colorBehind;

    private Outline outline;

    [SerializeField] private Distance currentDistance = Distance.undefined;

    private void Start()
    {
        outline = GetComponent<Outline>();
        CheckDistance();
    }

    void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (transform.position.z > distanceFar && currentDistance != Distance.far)
        {
            currentDistance = Distance.far;
            outline.OutlineColor = colorFar;
        }
        else if (transform.position.z > distanceMid && transform.position.z < distanceFar
            && currentDistance != Distance.mid)
        {
            currentDistance = Distance.mid;
            outline.OutlineColor = colorMid;
        }
        else if (transform.position.z > distanceClose && transform.position.z < distanceMid 
            && currentDistance != Distance.close)
        {
            currentDistance = Distance.close;
            outline.OutlineColor = colorClose;
        }
        else if (transform.position.z < distanceClose && currentDistance != Distance.behind)
        {
            currentDistance = Distance.behind;
            outline.OutlineColor = colorBehind;
        }
    }
}
