using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class StopEffect : MonoBehaviour
{
    private PooledObject pooledObj;

    private void Start()
    {
        pooledObj = GetComponent<PooledObject>();
    }

    private void OnParticleSystemStopped()
    {
        pooledObj.Release();
    }
}
