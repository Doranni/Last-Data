using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PooledObject))]
public class AsteroidController : MonoBehaviour
{
    private Health health;
    private PooledObject pooledObj;

    private void Start()
    {
        health = GetComponent<Health>();
        pooledObj = GetComponent<PooledObject>();
        health.OnDeath += Death;
    }

    private void Death()
    {
        EffectsController.ShowDustExplosionEffect(transform.position);
        pooledObj.Release();
    }
}
