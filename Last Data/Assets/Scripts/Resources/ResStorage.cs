using UnityEngine;
using System;

public abstract class ResStorage
{
    public virtual string Name => "Resource";
    public float CurrentAmount { get; private set; }
    public float Capacity { get; private set; }

    public event Action OnAmountChanged, OnCapacityChanged;

    public ResStorage(float capacity, float currentAmount)
    {
        Capacity = capacity;
        CurrentAmount = currentAmount;
    }

    public void ChangeAmount(float value)
    {
        CurrentAmount = Mathf.Clamp(CurrentAmount + value, 0, Capacity);
        OnAmountChanged?.Invoke();
    }

    public void ChangeCapasity(float value)
    {
        Capacity = Mathf.Clamp(Capacity + value, 0, Capacity + value);
        OnCapacityChanged?.Invoke();
    }
}
