using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private float movementRange;
    public float MoveForce => moveForce;
    public float MovementRange => movementRange;

    public MovementStateMachine StateMachine { get; private set; }

    private Rigidbody shipRigidbody;

    public Vector2 Input_Move { get; private set; }

    private void Awake()
    {
        shipRigidbody = GetComponent<Rigidbody>();
        StateMachine = new MovementStateMachine(this, shipRigidbody);
        StateMachine.Initialize(StateMachine.movingState);
    }

    void Start()
    {
        InputManager.Instance.OnMove_performed += Move;
    }

    

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Input_Move = obj.ReadValue<Vector2>();
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnMove_performed -= Move;
    }
}
