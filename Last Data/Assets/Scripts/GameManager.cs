using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float speedMin, speedMax, accelerationTime;
    [SerializeField] private Fuel shipFuel;
    [SerializeField] private float fuelConsumptionMax, fuelConsumptionMin;
    [SerializeField] private Vector3 astSpawnRange_min, astSpawnRange_max;
    [SerializeField] private Vector2 objDetectRange_min, objDetectRange_max;

    public float Speed { get; private set; }
    public float SpeedGoal { get; private set; }
    private bool input_Acceleration;
    private float accelerationStep;
    private float speedMultiplier;
    private const float speedMaxGoal = 163;

    public Vector3 AstSpawnRange_min => astSpawnRange_min;
    public Vector3 AstSpawnRange_max => astSpawnRange_max;
    public Vector2 ObjDetectRange_min => objDetectRange_min;
    public Vector2 ObjDetectRange_max => objDetectRange_max;
    public Vector3 ObjExistRange_min { get; private set; }
    public Vector3 ObjExistRange_max { get; private set; }

    public readonly string tag_asteroids = "Asteroid", tag_ship = "Ship";

    public event Action OnSpeedChanged;

    public override void Awake()
    {
        base.Awake();
        speedMultiplier = speedMaxGoal / speedMax;
        Speed = speedMin;
        accelerationStep = (speedMax - speedMin) / accelerationTime;
        ObjExistRange_min = new Vector3(astSpawnRange_min.x - 10, astSpawnRange_min.y - 10, -50);
        ObjExistRange_max = new Vector3(astSpawnRange_max.x + 10, astSpawnRange_max.y + 10, astSpawnRange_max.z + 10);
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
        shipFuel.ChangeFuelAmount(
            -(Mathf.Lerp(fuelConsumptionMax, fuelConsumptionMin, Mathf.InverseLerp(speedMin, speedMax, Speed))
            * Time.deltaTime));
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnAcceleration_started -= _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled -= _ => Acceleration_Cancel();
    }
}
