using UnityEngine;

public class UraniumStorage : ResStorage
{
    public override string Name => "Uranium";

    public UraniumStorage(float capacity, float currentAmount) : base (capacity, currentAmount) { }
}
