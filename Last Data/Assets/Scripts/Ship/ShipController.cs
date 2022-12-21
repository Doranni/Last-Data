using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            Debug.Log("We hit asteroid");
        }
        else
        {
            Debug.Log("We hit something");
        }
    }
}
