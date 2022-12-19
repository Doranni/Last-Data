using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private float movementRangeX, movementRangeY;
    [SerializeField] private float speedMin, speedMax, accelerationTime;

    private Rigidbody shipRigidbody;

    private Vector2 input_Move;
    private bool input_Acceleration;

    public float Speed { get; private set; }
    private float accelerationStep;

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InputManager.Instance.OnMove_performed += Move_Perform;
        InputManager.Instance.OnAcceleration_started += _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled += _ => Acceleration_Cancel();

        Speed = speedMin;
        accelerationStep = (speedMax - speedMin) / accelerationTime;
    }

    private void Acceleration_Cancel()
    {
        input_Acceleration = false;
    }

    private void Acceleration_Start()
    {
        input_Acceleration = true;
    }

    private void Move_Perform(InputAction.CallbackContext obj)
    {
        input_Move = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if ((transform.position.x > movementRangeX && shipRigidbody.velocity.x > 0)
            || (transform.position.x < -movementRangeX && shipRigidbody.velocity.x < 0))
        {
            shipRigidbody.velocity = new Vector3(0, shipRigidbody.velocity.y, shipRigidbody.velocity.z);
        }
        if ((transform.position.y > movementRangeY && shipRigidbody.velocity.y > 0)
            || (transform.position.y < -movementRangeY && shipRigidbody.velocity.y < 0))
        {
            shipRigidbody.velocity = new Vector3(shipRigidbody.velocity.x, 0, shipRigidbody.velocity.z);
        }
        else
        {
            shipRigidbody.AddForce(input_Move.x * moveForce, input_Move.y * moveForce, 0);
        }
    }

    private void Update()
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
    }

    

    private void OnDestroy()
    {
        InputManager.Instance.OnMove_performed -= Move_Perform;
        InputManager.Instance.OnAcceleration_started -= _ => Acceleration_Start();
        InputManager.Instance.onAcceleration_canceled -= _ => Acceleration_Cancel();
    }
}
