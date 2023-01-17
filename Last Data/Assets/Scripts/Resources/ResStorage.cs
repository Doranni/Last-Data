using UnityEngine;
using System;

public class ResStorage
{
    public string Name { get; private set; }
    public ResourceType Type { get; private set; }
    public float CurrentAmount { get; private set; }
    public float Capacity { get; private set; }

    public ResStorage(string name, ResourceType type, float capacity, float currentAmount)
    {
        Name = name;
        Type = type;
        Capacity = capacity;
        CurrentAmount = currentAmount;
    }

    public void ChangeAmount(float value)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount + value, 0, Capacity);
    }

    public void ChangeCapasity(float value)
    {
        Capacity = Mathf.Clamp(Capacity + value, 0, Capacity + value);
    }
}
