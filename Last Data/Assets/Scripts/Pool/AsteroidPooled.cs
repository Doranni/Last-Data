using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Rigidbody))]
public class AsteroidPooled : PooledObject
{
    private float scale, mass;

    private Health astHealth;
    private Damage astDamage;
    private Rigidbody astRigidbody;

    private const float healthMultiplier = 0.5f, damageMultiplier = 0.3f;
    private const float scaleMin = 3f, scaleMax = 3f, massMultiplier = 100;

    private void Awake()
    {
        astHealth = GetComponent<Health>();
        astDamage = GetComponent<Damage>();
        astRigidbody = GetComponent<Rigidbody>();
    }

    public override void Initialize()
    {
        scale = Random.Range(scaleMin, scaleMax);
        mass = scale * massMultiplier;
        transform.localScale = Vector3.one * scale;
        astRigidbody.mass = mass;
        astHealth.SetMaxHealth(mass * healthMultiplier);
        astDamage.SetPassiveDamage(mass * damageMultiplier);
    }
}
