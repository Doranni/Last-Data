using UnityEngine;

public class ShipController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(GameManager.Instance.tag_meteorites))
        {
            Debug.Log("We hit asteroid");
        }
        else
        {
            Debug.Log("We hit something");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.Instance.tag_resources))
        {
            Debug.Log("We hit resource");
        }
    }
}
