using UnityEngine;

public class ShipStorage : Singleton<ShipStorage>
{
    [SerializeField] private float laserChargeCapacity = 80, fuelCapacity = 400,
        metalsCapacity = 200, uraniumCapacity = 50, rawDataCapacity = 100;

    public LaserChargeStorage LaserCharge { get; private set; }
    public FuelStorage Fuel { get; private set; }
    public MetalsStorage Metals { get; private set; }
    public UraniumStorage Uranium { get; private set; }
    public RawDataStorage RawData { get; private set; }

    public override void Awake()
    {
        base.Awake();
        LaserCharge = new LaserChargeStorage(laserChargeCapacity, laserChargeCapacity);
        Fuel = new FuelStorage(fuelCapacity, fuelCapacity);
        Metals = new MetalsStorage(metalsCapacity, metalsCapacity / 3);
        Uranium = new UraniumStorage(uraniumCapacity, uraniumCapacity / 3);
        RawData = new RawDataStorage(rawDataCapacity, rawDataCapacity / 3);
    }
}


