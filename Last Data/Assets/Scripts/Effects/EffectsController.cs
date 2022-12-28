using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsController : MonoBehaviour
{
    [SerializeField] private ObjectPool asteroidExplosionPool, shipDamagePool;

    private static EffectsController instance;

    private void Awake()
    {
        instance = this;
    }

    public static void PlayAsteroidExplosionEffect(Vector3 position)
    {
        instance.PlayAsteroidExplosionEffect_private(position);
    }

    private void PlayAsteroidExplosionEffect_private(Vector3 position)
    {
        ParticleSystem effect = asteroidExplosionPool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play(true);
    }

    public static void PlayShipDamageEffect(Vector3 position)
    {
        instance.PlayShipDamageEffect_private(position);
    }

    private void PlayShipDamageEffect_private(Vector3 position)
    {
        ParticleSystem effect = shipDamagePool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play(true);
    }
}
