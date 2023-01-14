using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float speedMin = 5, speedMax = 30, accelerationTime = 5;
    [SerializeField] private float fuelConsumptionMax = 0.1f, fuelConsumptionMin = 0.05f;
    [SerializeField] private Vector3 meteoritesSpawnRange_min = new (-400, -200, 50), 
        meteoritesSpawnRange_max = new (400, 200, 1000);
    [SerializeField] private Vector2 objDetectRange_min = new (-50, -30), objDetectRange_max = new (50, 30);

    public float Speed { get; private set; }
    public float SpeedGoal { get; private set; }
    private bool input_Acceleration;
    private float accelerationStep;
    private float speedMultiplier;
    private const float speedMaxGoal = 163;

    public Vector3 MeteoriteSpawnRange_min => meteoritesSpawnRange_min;
    public Vector3 MeteoriteSpawnRange_max => meteoritesSpawnRange_max;
    public Vector2 ObjDetectRange_min => objDetectRange_min;
    public Vector2 ObjDetectRange_max => objDetectRange_max;
    public Vector3 ObjExistRange_min { get; private set; }
    public Vector3 ObjExistRange_max { get; private set; }

    public readonly string tag_meteorites = "Meteorite", tag_ship = "Ship";

    public event Action OnSpeedChanged;

    public override void Awake()
    {
        base.Awake();
        speedMultiplier = speedMaxGoal / speedMax;
        Speed = speedMin;
        accelerationStep = (speedMax - speedMin) / accelerationTime;
        ObjExistRange_min = new Vector3(meteoritesSpawnRange_min.x - 10, meteoritesSpawnRange_min.y - 10, -50);
        ObjExistRange_max = new Vector3(meteoritesSpawnRange_max.x + 10, meteoritesSpawnRange_max.y + 10, 
            meteoritesSpawnRange_max.z + 10);
    }

    private void Start()
    {
        InputManager.Instance.OnAcceleration_started += _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled += _ => Acceleration_Cancel();

        Cursor.visible = false;
    }

    private void Acceleration_Cancel()
    {
        input_Acceleration = false;
    }

    private void Acceleration_Start()
    {
        input_Acceleration = true;
    }

    void Update()
    {
        UpdateSpeed();
        UseFuel();
    }

    private void UpdateSpeed()
    {
        if (input_Acceleration && Speed < speedMax)
        {
            Speed += accelerationStep * Time.deltaTime;
        }
        else if (!input_Acceleration && Speed > speedMin)
        {
            Speed -= accelerationStep * Time.deltaTime;
        }
        Speed = Mathf.Clamp(Speed, speedMin, speedMax);
        SpeedGoal = Speed * speedMultiplier;
        OnSpeedChanged?.Invoke();
    }

    private void UseFuel()
    {
        ShipStorage.Instance.Fuel.ChangeAmount(
            -(Mathf.Lerp(fuelConsumptionMax, fuelConsumptionMin, Mathf.InverseLerp(speedMin, speedMax, Speed))
            * Time.deltaTime));
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnAcceleration_started -= _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled -= _ => Acceleration_Cancel();
    }
}
