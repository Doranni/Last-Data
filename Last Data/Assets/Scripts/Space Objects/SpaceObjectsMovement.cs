using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectsMovement : MonoBehaviour
{
    [SerializeField] private ShipMovement shipMovement;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.back * shipMovement.Speed);
    }
}
