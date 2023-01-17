using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShowDistance))]
public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    public ResourceType Type => type;

    [SerializeField] private float amount;
    public float Amount => amount;

    public bool IsUnderInteraction { get; private set; }

    private ShowDistance showDistance;

    private void Awake()
    {
        showDistance = GetComponent<ShowDistance>();
        IsUnderInteraction = false;
    }

    public void VacuumTargetSet()
    {
        IsUnderInteraction = true;
        showDistance.StartInteraction();
    }

    public void VacuumTargetRelease()
    {
        IsUnderInteraction = false;
        showDistance.StopInteraction();
    }
}

public enum ResourceType
{
    charge,
    fuel,
    metals,
    uranium,
    rawData
}
