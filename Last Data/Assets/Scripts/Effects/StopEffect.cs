using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class StopEffect : MonoBehaviour
{
    private PooledObject pooledObj;

    private void Awake()
    {
        pooledObj = GetComponent<PooledObject>();
    }

    private void OnParticleSystemStopped()
    {
        pooledObj.Release();
    }
}
