using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageble : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagebleHit;
    public UnityEvent<int, int> healthChanged;
    public UnityEvent onDeathEvent;
    private Animator animator;

    [SerializeField] private int maxHealth = 100;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    [SerializeField] private int currentHealth = 100;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            healthChanged?.Invoke(currentHealth, maxHealth);
            if (currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private bool isAlive = true;
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            if (!isAlive)
            {
                onDeathEvent?.Invoke();
            }
        }
    }

    public bool LockVelocity
    {
        get => animator.GetBool(AnimationStrings.lockVelocity);
        set => animator.SetBool(AnimationStrings.lockVelocity, value);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            CurrentHealth -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damagebleHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }

        return false;
    }

    public bool Heal(int health)
    {
        if (IsAlive && CurrentHealth < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - CurrentHealth, 0);
            int heal = Mathf.Min(maxHeal, health);
            CurrentHealth += heal;
            CharacterEvents.characterHealed.Invoke(gameObject, heal);
            return true;
        }

        return false;
    }
}