using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(Rigidbody))]
public class MeteoritePooled : PooledObject
{
    private float scale, mass;

    private const float healthMultiplier = 10, damageMultiplier = 30;
    private const float scaleMin = 0.8f, scaleMax = 15f, massMultiplier = 100;

    private Health mHealth;
    private Damage mDamage;
    private Rigidbody mRigidbody;

    private void Awake()
    {
        mHealth = GetComponent<Health>();
        mDamage = GetComponent<Damage>();
        mRigidbody = GetComponent<Rigidbody>();
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
        mRigidbody.mass = mass;
        mHealth.SetMaxHealth(scale * healthMultiplier);
        mDamage.SetPassiveDamage(scale * damageMultiplier);
    }
}
