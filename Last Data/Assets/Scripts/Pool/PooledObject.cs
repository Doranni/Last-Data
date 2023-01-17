using System;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool { get; private set; }

    public virtual void Initialize(ObjectPool pool)
    {
        Pool = pool;
        gameObject.SetActive(false);
    }

    public virtual PooledObject Get()
    {
        gameObject.SetActive(true);
        return this;
    }

    public virtual void Release()
    {
        if (Pool == null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Pool.ReturnToPool(this);
        }
    }
}
