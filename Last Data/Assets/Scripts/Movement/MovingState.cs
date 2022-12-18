using UnityEngine;
using UnityEngine.InputSystem;

public class MovingState : IMovementState
{
    private MovementController mController;
    private Rigidbody shipRB;
    private Transform shipTransform;

    public MovingState(MovementController movementController, Rigidbody shipRigidbody)
    {
        this.mController = movementController;
        this.shipRB = shipRigidbody;
        shipTransform = shipRigidbody.gameObject.transform;
    }

    public void FixedUpdate()
    {
        if ((shipTransform.position.x > mController.MovementRange &&
            shipRB.velocity.x > 0)
            || (shipTransform.position.x < -mController.MovementRange &&
            shipRB.velocity.x < 0))
        {
            shipRB.velocity = Vector3.zero;
        }
        if ((shipTransform.position.x > mController.MovementRange &&
            mController.Input_Move.x > 0)
            || (shipTransform.position.x < - mController.MovementRange &&
            mController.Input_Move.x < 0))
        {
            Debug.Log("You need to move towards your quest!");
        }
        else
        {
            shipRB.AddForce(mController.Input_Move.x * mController.MoveForce, 0, 0);
        }
    }
}
