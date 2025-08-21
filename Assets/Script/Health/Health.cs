using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [field:SerializeField] public uint CurrentHealth { get; private set; }
    [field:SerializeField] public uint MaxHealth { get; private set; }
    public UnityEvent OnDamagedEvent = new();
    
    public void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    
    public void Damage(uint damage)
    {
        CurrentHealth -= CurrentHealth > damage ? damage : CurrentHealth;
        OnDamagedEvent.Invoke();
    }
}
