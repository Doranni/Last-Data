using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float passiveDamage;

    public float PassiveDamage => passiveDamage;

    public void SetPassiveDamage(float value)
    {
        passiveDamage = value;
    }
}
