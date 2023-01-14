using UnityEngine;

public class FuelStorage : ResStorage
{
    public override string Name => "Fuel";

    public FuelStorage(float capacity, float currentAmount) : base(capacity, currentAmount) { }
}
