using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawDataStorage : ResStorage
{
    public override string Name => "Raw Data";

    public RawDataStorage(float capacity, float currentAmount) : base(capacity, currentAmount) { }
}
