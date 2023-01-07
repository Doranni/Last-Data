using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PooledObject))]
[RequireComponent(typeof(ShowDistance))]
public class AsteroidController : MonoBehaviour
{
    

    private Health astHealth;
    private PooledObject pooledObj;
    private ShowDistance showDistance;

    private void Awake()
    {
        astHealth = GetComponent<Health>();
        pooledObj = GetComponent<PooledObject>();
        showDistance = GetComponent<ShowDistance>();
    }

    private void Start()
    {
        astHealth.OnGetDamage += GetDamage;
        astHealth.OnDeath += Death;
    }

    private void GetDamage((float currentHealth, float maxHealth) obj)
    {
        
    }

    private void Death()
    {
        EffectsController.PlayAsteroidExplosionEffect(transform.position);
        Debug.Log($"Ast: {name} Death, position - {transform.position}, health - {astHealth.CurrentHealth}.");
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
        astHealth.ChangeHealth(value);
    }

    public (float currentHealth, float maxHealth) GetHealth()
    {
        return (astHealth.CurrentHealth, astHealth.MaxHealth);
    }

    public bool IsDead()
    {
        return astHealth.IsDead;
    }

    public void Respawn()
    {
        astHealth.Respawn();
    }
}
