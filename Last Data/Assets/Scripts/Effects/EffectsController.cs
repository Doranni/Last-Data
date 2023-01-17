using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsController : MonoBehaviour
{
    [SerializeField] private ObjectPool meteoriteExplosionPool, shipDamagePool;

    private static EffectsController instance;

    private void Awake()
    {
        instance = this;
    }

    public static void PlayMeteoriteExplosionEffect(Vector3 position)
    {
        instance.PlayMeteoriteExplosionEffect_private(position);
    }

    private void PlayMeteoriteExplosionEffect_private(Vector3 position)
    {
        ParticleSystem effect = meteoriteExplosionPool.GetPooledObject().GetComponent<ParticleSystem>();
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
