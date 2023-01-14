using UnityEngine;

public class LaserChargeStorage : ResStorage
{
    public override string Name => "Laser Charge";

    public LaserChargeStorage(float capacity, float currentAmount) : base(capacity, currentAmount) { }
}
