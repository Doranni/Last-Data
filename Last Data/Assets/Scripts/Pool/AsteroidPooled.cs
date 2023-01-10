using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Rigidbody))]
public class AsteroidPooled : PooledObject
{
    private float scale, mass;

    private const float healthMultiplier = 0.5f, damageMultiplier = 0.3f;
    private const float scaleMin = 0.8f, scaleMax = 3f, massMultiplier = 100;

    private Health astHealth;
    private Damage astDamage;
    private Rigidbody astRigidbody;

    private void Awake()
    {
        astHealth = GetComponent<Health>();
        astDamage = GetComponent<Damage>();
        astRigidbody = GetComponent<Rigidbody>();
    }

    public override PooledObject Get()
    {
        Randomize();
        return base.Get();
    }

    private void Randomize()
    {
        scale = Random.Range(scaleMin, scaleMax);
        mass = scale * massMultiplier;
        transform.localScale = Vector3.one * scale;
        astRigidbody.mass = mass;
        astHealth.SetMaxHealth(mass * healthMultiplier);
        astDamage.SetPassiveDamage(mass * damageMultiplier);
    }
}
