using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject[] objectToPool;

    private List<PooledObject> list;

    private void Start()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        list = new List<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool[Random.Range(0, objectToPool.Length)], gameObject.transform);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            list.Add(instance);
        }
    }

    public PooledObject GetPooledObject()
    {
        if (list.Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool[Random.Range(0, objectToPool.Length)], gameObject.transform);
            newInstance.Pool = this;
            return newInstance;
        }
        int index = Random.Range(0, list.Count);
        PooledObject nextInstance = list[index];
        list.RemoveAt(index);
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }
    public void ReturnToPool(PooledObject pooledObject)
    {
        list.Add(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
