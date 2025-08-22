using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [field:SerializeField] public uint CurrentHealth { get; private set; }
    [field:SerializeField] public uint MaxHealth { get; private set; }
    public UnityEvent OnDamagedEvent = new();
    public UnityEvent OnHealedEvent = new();
    public UnityEvent OnDeathEvent = new();
    public UnityEvent OnReviveEvent = new();
    public bool IsAlive => CurrentHealth > 0;
    public bool IsInvulnerable { get; private set; }
    public void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    
    public void Damage(uint damage)
    {
        if (IsInvulnerable)
            return;
        bool currentAliveState = IsAlive;
        CurrentHealth -= CurrentHealth > damage ? damage : CurrentHealth;
        OnDamagedEvent.Invoke();
        if (currentAliveState != IsAlive)
        {
            OnDeathEvent.Invoke();
        }
    }
    public void Heal(uint heal)
    {
        bool currentAliveState = IsAlive;
        CurrentHealth += CurrentHealth < MaxHealth ? heal : MaxHealth;
        OnHealedEvent.Invoke();
        if (currentAliveState != IsAlive)
        {
            OnReviveEvent.Invoke();
        }
    }
    void MakeInvulnerable()
    {
        IsInvulnerable = true;
    }

    void MakeVulnerable()
    {
        IsInvulnerable = false;
    }
    
}
