using System;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private GameInput gameInput;

    public event Action<InputAction.CallbackContext> OnMenu_performed;

    public override void Awake()
    {
        base.Awake();
        gameInput = new GameInput();

    }

    private void Start()
    {
        gameInput.Ship.Move.performed += Move_performed;

        
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        OnMenu_performed?.Invoke(obj);
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

    }
}
