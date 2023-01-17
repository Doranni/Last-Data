using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipStorage : Singleton<ShipStorage>
{
    [SerializeField] private float laserChargeCapacity = 80, fuelCapacity = 400,
        metalsCapacity = 200, uraniumCapacity = 50, rawDataCapacity = 100;
    [SerializeField] private float fuelConsumptionMax = 0.1f, fuelConsumptionMin = 0.05f,
        laserChargeConsumption = 0.5f, vacuumChargeConsumption = 0.5f;

    private Dictionary<ResourceType, ResStorage> storages = new();

    public event Action<(float currentAmount, float capacity)> OnMetalsChanged, OnUraniumChanges, OnRawDataChanged;

    public override void Awake()
    {
        base.Awake();
        storages.Add(ResourceType.charge, 
            new ResStorage("Charge", ResourceType.charge, laserChargeCapacity, laserChargeCapacity));
        storages.Add(ResourceType.fuel, 
            new ResStorage("Fuel", ResourceType.fuel, fuelCapacity, fuelCapacity));
        storages.Add(ResourceType.metals, 
            new ResStorage("Metals", ResourceType.metals, metalsCapacity, metalsCapacity / 3));
        storages.Add(ResourceType.uranium, 
            new ResStorage("Uranium", ResourceType.uranium, uraniumCapacity, uraniumCapacity / 3));
        storages.Add(ResourceType.rawData, 
            new ResStorage("Raw Data", ResourceType.rawData, rawDataCapacity, rawDataCapacity / 3));
    }

    public void GatherResource(GameObject obj)
    {
        if (obj.TryGetComponent(out Resource resource))
        {
            storages[resource.Type].ChangeAmount(resource.Amount);
            switch (resource.Type)
            {
                case ResourceType.metals:
                    {
                        OnMetalsChanged?.Invoke((storages[ResourceType.metals].CurrentAmount, 
                            storages[ResourceType.metals].Capacity));
                        break;
                    }
                case ResourceType.uranium:
                    {
                        OnUraniumChanges?.Invoke((storages[ResourceType.uranium].CurrentAmount,
                            storages[ResourceType.uranium].Capacity));
                        break;
                    }
                case ResourceType.rawData:
                    {
                        OnRawDataChanged?.Invoke((storages[ResourceType.rawData].CurrentAmount,
                            storages[ResourceType.rawData].Capacity));
                        break;
                    }
            }
        }
    }

    public void PowerLaser()
    {
        storages[ResourceType.charge].ChangeAmount(-laserChargeConsumption * Time.deltaTime);
    }

    public void PowerVacuum()
    {
        storages[ResourceType.charge].ChangeAmount(-vacuumChargeConsumption * Time.deltaTime);
    }

    public void UseFuel()
    {
        storages[ResourceType.fuel].ChangeAmount(-Mathf.Lerp(fuelConsumptionMax, fuelConsumptionMin, 
            Mathf.InverseLerp(GameManager.Instance.SpeedMin, GameManager.Instance.SpeedMax, GameManager.Instance.SpeedReal))
            * Time.deltaTime);
    }

    public bool HasPower()
    {
        return storages[ResourceType.charge].CurrentAmount > 0;
    }

    public (float currentAmount, float capacity) ShowResAmount(ResourceType type)
    {
        return (storages[type].CurrentAmount, storages[type].Capacity);
    }
}


