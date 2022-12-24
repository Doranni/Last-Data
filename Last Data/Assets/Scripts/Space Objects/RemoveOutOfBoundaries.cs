using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class RemoveOutOfBoundaries : MonoBehaviour
{
    private PooledObject pooledObj;

    private void Start()
    {
        pooledObj = GetComponent<PooledObject>();
    }

    void Update()
    {
        if (transform.position.x > GameManager.Instance.ObjExistRange_max.x
            || transform.position.x < GameManager.Instance.ObjExistRange_min.x
            || transform.position.y > GameManager.Instance.ObjExistRange_max.y
            || transform.position.y < GameManager.Instance.ObjExistRange_min.y
            || transform.position.z > GameManager.Instance.ObjExistRange_max.z
            || transform.position.z < GameManager.Instance.ObjExistRange_min.z)
        {

            pooledObj.Release();
        }
    }
}
