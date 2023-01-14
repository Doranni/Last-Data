using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PooledObject))]
[RequireComponent(typeof(ShowDistance))]
public class MeteoriteController : MonoBehaviour
{
    private Health mHealth;
    private PooledObject pooledObj;
    private ShowDistance showDistance;

    private void Awake()
    {
        mHealth = GetComponent<Health>();
        pooledObj = GetComponent<PooledObject>();
        showDistance = GetComponent<ShowDistance>();
    }

    private void Start()
    {
        mHealth.OnGetDamage += GetDamage;
        mHealth.OnDeath += Death;
    }

    private void GetDamage((float currentHealth, float maxHealth) obj)
    {
        
    }

    private void Death()
    {
        EffectsController.PlayAsteroidExplosionEffect(transform.position);
        Debug.Log($"Meteorite: {name} Death, position - {transform.position}, health - {mHealth.CurrentHealth}.");
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
        mHealth.ChangeHealth(value);
    }

    public (float currentHealth, float maxHealth) GetHealth()
    {
        return (mHealth.CurrentHealth, mHealth.MaxHealth);
    }

    public bool IsDead()
    {
        return mHealth.IsDead;
    }

    public void Respawn()
    {
        mHealth.Respawn();
    }
}
