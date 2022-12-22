using UnityEngine;

public class ShipController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(GameManager.Instance.tag_asteroids))
        {
            Debug.Log("We hit asteroid");
        }
        else
        {
            Debug.Log("We hit something");
        }
    }
}
