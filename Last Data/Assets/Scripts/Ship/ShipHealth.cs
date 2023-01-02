using System;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField]
    private Health[] shipParts;

    public event Action<(float currentValue, float maxValue)> OnChangeHealth;

    private void Start()
    {
        for (int i = 0; i < shipParts.Length; i++)
        {
            shipParts[i].OnChangeHealth += _ => HealthChanged();
        }
    }

    public (float currentValue, float maxValue) GetTotalHealth()
    {
        float currentValue = 0, maxValue = 0;
        for (int i = 0; i < shipParts.Length; i++)
        {
            currentValue += shipParts[i].CurrentHealth;
            maxValue += shipParts[i].MaxHealth;
        }
        return (currentValue, maxValue);
    }

    private void HealthChanged()
    {
        OnChangeHealth?.Invoke(GetTotalHealth());
    }
}
