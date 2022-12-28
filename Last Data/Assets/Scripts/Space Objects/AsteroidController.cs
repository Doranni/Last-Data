using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PooledObject))]
[RequireComponent(typeof(ShowDistance))]
public class AsteroidController : MonoBehaviour
{
    private Health health;
    private PooledObject pooledObj;
    private ShowDistance showDistance;

    private void Awake()
    {
        health = GetComponent<Health>();
        pooledObj = GetComponent<PooledObject>();
        showDistance = GetComponent<ShowDistance>();
    }

    private void Start()
    {
        health.OnGetDamage += GetDamage;
        health.OnDeath += Death;
    }

    private void GetDamage((float currentHealth, float maxHealth) obj)
    {
        
    }

    private void Death()
    {
        EffectsController.PlayAsteroidExplosionEffect(transform.position);
        Debug.Log($"Ast: {name} Death, position - {transform.position}, health - {health.CurrentHealth}.");
        pooledObj.Release();
        showDistance.ClearOutlining();
    }

    public void LaserTargetSet()
    {
        showDistance.StartInteraction();
    }

    public void LaserTargetRelease()
    {
        showDistance.StopInteraction();
    }

    public void ChangeHealth(float value)
    {
        health.ChangeHealth(value);
    }

    public (float currentHealth, float maxHealth) GetHealth()
    {
        return (health.CurrentHealth, health.MaxHealth);
    }

    public bool IsDead()
    {
        return health.IsDead;
    }

    public void Respawn()
    {
        health.Respawn();
    }
}
