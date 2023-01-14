using UnityEngine;

public class MetalsStorage : ResStorage
{
    public override string Name => "Metals";

    public MetalsStorage(float capacity, float currentAmount) : base (capacity, currentAmount) { }
}
