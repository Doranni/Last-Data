using UnityEngine;

public class SpaceObjectsMovement : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    private Rigidbody SpaceOblectRB;

    void Start()
    {
        SpaceOblectRB = GetComponent<Rigidbody>();
        SpaceOblectRB.angularVelocity = Random.insideUnitSphere * rotSpeed;
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.back * GameManager.Instance.Speed * Time.deltaTime, Space.World);
    }
}
