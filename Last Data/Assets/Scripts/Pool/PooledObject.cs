using System;
using UnityEngine;

public class PooledObject : MonoBehaviour 
{
    private ObjectPool pool;
    public ObjectPool Pool { get => pool; set => pool = value; }

    public void Release()
    {
        if (pool != null)
        {
            pool.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Initialize() { }

}
