using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float speedMin = 5, speedMax = 30, accelerationTime = 5;

    public float SpeedReal { get; private set; }
    public float SpeedMin => speedMin;
    public float SpeedMax => speedMax;
    public float SpeedToDisplay { get; private set; }
    private bool input_Acceleration;
    private float accelerationStep;
    private float speedMultiplier;
    private const float speedToDisplayMax = 163;

    public event Action OnSpeedChanged;

    [SerializeField] private Vector3 meteoritesSpawnRange_min = new(-400, -200, 50),
        meteoritesSpawnRange_max = new(400, 200, 1000);
    [SerializeField] private Vector2 objDetectRange_min = new(-50, -30), objDetectRange_max = new(50, 30);

    public Vector3 MeteoriteSpawnRange_min => meteoritesSpawnRange_min;
    public Vector3 MeteoriteSpawnRange_max => meteoritesSpawnRange_max;
    public Vector2 ObjDetectRange_min => objDetectRange_min;
    public Vector2 ObjDetectRange_max => objDetectRange_max;
    public Vector3 ObjExistRange_min { get; private set; }
    public Vector3 ObjExistRange_max { get; private set; }

    public readonly string tag_meteorites = "Meteorite", tag_ship = "Ship", tag_resources = "Resource";

    [SerializeField] private Color colorFar_meteorite, colorMid_meteorite, colorClose_meteorite,
        colorFar_resource, colorMid_resource, colorClose_resource, colorInteracted;
    private Color colorTransparent = new Color(0, 0, 0, 0);

    public Color ColorFar_meteorite => colorFar_meteorite;
    public Color ColorMid_meteorite => colorMid_meteorite;
    public Color ColorClose_meteorite => colorClose_meteorite;
    public Color ColorFar_resource => colorFar_resource;
    public Color ColorMid_resource => colorMid_resource;
    public Color ColorClose_resource => colorClose_resource;
    public Color ColorInteracted => colorInteracted;
    public Color ColorTransparent => colorTransparent;

    [SerializeField] private float distanceUnreachable_meteorite, distanceFar_meteorite, 
        distanceMid_meteorite, distanceClose_meteorite,
        distanceUnreachable_resource, distanceFar_resource, distanceMid_resource, distanceClose_resource;

    public float DistanceUnreachable_meteorite => distanceUnreachable_meteorite;
    public float DistanceFar_meteorite => distanceFar_meteorite;
    public float DistanceMid_meteorite => distanceMid_meteorite;
    public float DistanceClose_meteorite => distanceClose_meteorite;
    public float DistanceUnreachable_resource => distanceUnreachable_resource;
    public float DistanceFar_resource => distanceFar_resource;
    public float DistanceMid_resource => distanceMid_resource;
    public float DistanceClose_resource => distanceClose_resource;

    public override void Awake()
    {
        base.Awake();
        speedMultiplier = speedToDisplayMax / speedMax;
        SpeedReal = speedMin;
        accelerationStep = (speedMax - speedMin) / accelerationTime;
        ObjExistRange_min = new Vector3(meteoritesSpawnRange_min.x - 10, meteoritesSpawnRange_min.y - 10, -50);
        ObjExistRange_max = new Vector3(meteoritesSpawnRange_max.x + 10, meteoritesSpawnRange_max.y + 10, 
            meteoritesSpawnRange_max.z + 10);
    }

    private void Start()
    {
        InputManager.Instance.OnAcceleration_started += _ => Acceleration_Start();
        InputManager.Instance.OnAcceleration_canceled += _ => Acceleration_Cancel();

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
        ShipStorage.Instance.UseFuel(); ;
    }

    private void UpdateSpeed()
    {
        if (input_Acceleration && SpeedReal < speedMax)
        {
            SpeedReal += accelerationStep * Time.deltaTime;
        }
        else if (!input_Acceleration && SpeedReal > speedMin)
        {
            SpeedReal -= accelerationStep * Time.deltaTime;
        }
        SpeedReal = Mathf.Clamp(SpeedReal, speedMin, speedMax);
        SpeedToDisplay = SpeedReal * speedMultiplier;
        OnSpeedChanged?.Invoke();
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnAcceleration_started -= _ => Acceleration_Start();
        InputManager.Instance.OnAcceleration_canceled -= _ => Acceleration_Cancel();
    }
}
