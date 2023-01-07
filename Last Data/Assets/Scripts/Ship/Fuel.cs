using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] private float fuelCapasity;
    private float currentFuelAmount;

    public float CurrentFuelAmount => currentFuelAmount;
    public float FuelCapasity => fuelCapasity;

    private void Awake()
    {
        currentFuelAmount = fuelCapasity;
    }

    public void ChangeFuelAmount(float value)
    {
        currentFuelAmount = Mathf.Clamp(currentFuelAmount + value, 0, fuelCapasity);
    }
}
