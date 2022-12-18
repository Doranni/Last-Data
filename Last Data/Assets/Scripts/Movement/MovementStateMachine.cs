using System;
using UnityEngine;

public class MovementStateMachine 
{
    public IMovementState CurrentState { get; private set; }

    public MovingState movingState;
    public PausedState pausedState;

    public event Action OnMoving_started, OnMoving_canceled, OnStateChanged;

    public MovementStateMachine(MovementController movementController, Rigidbody shipRigidbody)
    {
        movingState = new MovingState(movementController, shipRigidbody);
        pausedState = new PausedState();
    }

    public void Initialize(IMovementState startingState)
    {
        CurrentState = startingState;
        OnStateChanged?.Invoke();
    }

    public void Start()
    {
        CurrentState.Enter();
    }

    public void TransitionTo(IMovementState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        OnStateChanged?.Invoke();
    }

    public void Update()
    {
        CurrentState?.Update();
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}
