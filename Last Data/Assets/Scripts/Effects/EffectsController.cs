using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectsController : MonoBehaviour
{
    [SerializeField] private ObjectPool asteroidExplosionPool, shipDamagePool;
    [SerializeField] private AudioClip asteroidExplosionAudio, shipDamageAudio;

    private AudioSource audioSource;

    private static EffectsController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayAsteroidExplosionEffect(Vector3 position)
    {
        instance.PlayAsteroidExplosionEffect_private(position);
    }

    private void PlayAsteroidExplosionEffect_private(Vector3 position)
    {
        ParticleSystem effect = asteroidExplosionPool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play();
        audioSource.PlayOneShot(asteroidExplosionAudio);
    }

    public static void PlayShipDamageEffect(Vector3 position)
    {
        instance.PlayShipDamageEffect_private(position);
    }

    private void PlayShipDamageEffect_private(Vector3 position)
    {
        ParticleSystem effect = shipDamagePool.GetPooledObject().GetComponent<ParticleSystem>();
        effect.transform.position = position;
        effect.Play();
        audioSource.PlayOneShot(shipDamageAudio);
    }
}
