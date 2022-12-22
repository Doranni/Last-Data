using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float moveForce = 30000;
    [SerializeField] private float moveRangeX = 42, moveRangeY = 25;

    private Rigidbody shipRigidbody;

    private Vector2 input_Move;

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        InputManager.Instance.OnMove_performed += Move_Perform;
    }

    private void Move_Perform(InputAction.CallbackContext obj)
    {
        input_Move = obj.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if ((transform.position.x > moveRangeX && shipRigidbody.velocity.x > 0)
            || (transform.position.x < -moveRangeX && shipRigidbody.velocity.x < 0))
        {
            shipRigidbody.velocity = new Vector3(0, shipRigidbody.velocity.y, shipRigidbody.velocity.z);
        }
        if ((transform.position.y > moveRangeY && shipRigidbody.velocity.y > 0)
            || (transform.position.y < -moveRangeY && shipRigidbody.velocity.y < 0))
        {
            shipRigidbody.velocity = new Vector3(shipRigidbody.velocity.x, 0, shipRigidbody.velocity.z);
        }
        else
        {
            shipRigidbody.AddForce(new Vector3(input_Move.x * moveForce, input_Move.y * moveForce, 0) * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnMove_performed -= Move_Perform;
    }
}
