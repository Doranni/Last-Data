using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Vector2 input_Move;

    void Start()
    {
        InputManager.Instance.OnMenu_performed += Move;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        input_Move = obj.ReadValue<Vector2>();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnMenu_performed -= Move;
    }
}
