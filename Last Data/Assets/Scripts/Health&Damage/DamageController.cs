using UnityEngine;

[RequireComponent(typeof(Damage))]
public class DamageController : MonoBehaviour
{
    private Damage damage;

    private void Awake()
    {
        damage = GetComponent<Damage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out Health collisionHealth))
        {
            collisionHealth.ChangeHealth(-damage.PassiveDamage);
            if (collision.gameObject.CompareTag(GameManager.Instance.tag_ship))
            {
                EffectsController.PlayShipDamageEffect(collision.GetContact(0).point);
            }
        }
    }
}
