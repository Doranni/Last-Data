using UnityEngine;

public class RemoveOutOfBoundaries : MonoBehaviour
{
    private bool isPooled;
    private PooledObject pObject;

    private void Start()
    {
        if (TryGetComponent<PooledObject>(out pObject))
        {
            isPooled = pObject.Pool != null;
        }
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
            if (isPooled)
            {
                pObject.Release();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
