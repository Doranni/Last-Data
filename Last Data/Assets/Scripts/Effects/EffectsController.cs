using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private ObjectPool dustExplosionPool, shipDamagePool;

    private static EffectsController instance;

    private void Awake()
    {
        instance = this;
    }

    public static void ShowDustExplosionEffect(Vector3 position)
    {
        instance.ShowDustExplosionEffect_private(position);
    }

    private void ShowDustExplosionEffect_private(Vector3 position)
    {
        ParticleSystem effect = dustExplosionPool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play();
    }

    public static void ShowShipDamageEffect(Vector3 position)
    {
        instance.ShowShipDamageEffect_private(position);
    }

    private void ShowShipDamageEffect_private(Vector3 position)
    {
        ParticleSystem effect = shipDamagePool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play();
    }
}
