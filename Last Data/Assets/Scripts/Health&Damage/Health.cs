using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public bool IsDead { get; private set; }

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public event Action<(float currentHealth, float maxHealth)> OnChangeHealth, OnGetDamage;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void ChangeHealth(float value, bool effectDead = false)
    {
        if (IsDead && !effectDead)
        {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        if (value < 0)
        {
            OnGetDamage?.Invoke((currentHealth, maxHealth));
        }
        OnChangeHealth?.Invoke((currentHealth, maxHealth));
        if (currentHealth == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }

    public void Respawn()
    {
        IsDead = false;
        currentHealth = maxHealth;
    }
}
