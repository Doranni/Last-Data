using System;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private GameInput gameInput;

    public event Action<InputAction.CallbackContext> OnMove_performed, 
        OnFireLeft_started, OnFireLeft_canceled, OnFireRight_started, OnFireRight_canceled,
        OnAcceleration_started, onAcceleration_canceled;

    public override void Awake()
    {
        base.Awake();
        gameInput = new GameInput();

    }

    private void Start()
    {
        gameInput.Ship.Move.performed += Move_performed;

        gameInput.Ship.FireLeft.started += FireLeft_started;
        gameInput.Ship.FireLeft.canceled += FireLeft_canceled;
        gameInput.Ship.FireRight.started += FireRight_started;
        gameInput.Ship.FireRight.canceled += FireRight_canceled;

        gameInput.Ship.Acceleration.started += Acceleration_started; 
        gameInput.Ship.Acceleration.canceled += Acceleration_canceled; 

    }

    private void Acceleration_canceled(InputAction.CallbackContext obj)
    {
        onAcceleration_canceled?.Invoke(obj);
    }

    private void Acceleration_started(InputAction.CallbackContext obj)
    {
        OnAcceleration_started?.Invoke(obj);
    }

    private void FireRight_canceled(InputAction.CallbackContext obj)
    {
        OnFireRight_canceled?.Invoke(obj);
    }

    private void FireRight_started(InputAction.CallbackContext obj)
    {
        OnFireRight_started?.Invoke(obj);
    }

    private void FireLeft_canceled(InputAction.CallbackContext obj)
    {
        OnFireLeft_canceled?.Invoke(obj);
    }

    private void FireLeft_started(InputAction.CallbackContext obj)
    {
        OnFireLeft_started?.Invoke(obj);
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        OnMove_performed?.Invoke(obj);
    }

    private void OnEnable()
    {
        gameInput.Enable();
    }

    private void OnDisable()
    {
        gameInput.Disable();
    }

    private void OnDestroy()
    {
        gameInput.Ship.Move.performed -= Move_performed;
        gameInput.Ship.FireLeft.started -= FireLeft_started;
        gameInput.Ship.FireLeft.canceled -= FireLeft_canceled;
        gameInput.Ship.Acceleration.started -= Acceleration_started;
        gameInput.Ship.Acceleration.canceled -= Acceleration_canceled;
    }
}
