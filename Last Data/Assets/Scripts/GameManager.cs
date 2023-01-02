using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float speedMin, speedMax, accelerationTime;
    [SerializeField] private Vector2 objDetectRange_min, objDetectRange_max;
    [SerializeField] private Vector3 objExistRange_min, objExistRange_max;

    public float Speed { get; private set; }
    public float SpeedGoal { get; private set; }
    private bool input_Acceleration;
    private float accelerationStep;
    private float speedMultiplier;
    private const float speedMaxGoal = 163;

    public Vector2 ObjDetectRange_min => objDetectRange_min;
    public Vector2 ObjDetectRange_max => objDetectRange_max;
    public Vector3 ObjExistRange_min => objExistRange_min;
    public Vector3 ObjExistRange_max => objExistRange_max;

    public readonly string tag_asteroids = "Asteroid", tag_ship = "Ship";

    public event Action OnSpeedChanged;

    void Start()
    {
        InputManager.Instance.OnAcceleration_started += _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled += _ => Acceleration_Cancel();

        speedMultiplier = speedMaxGoal / speedMax;
        Speed = speedMin;
        accelerationStep = (speedMax - speedMin) / accelerationTime;

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

    private void OnDestroy()
    {
        InputManager.Instance.OnAcceleration_started -= _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled -= _ => Acceleration_Cancel();
    }
}
