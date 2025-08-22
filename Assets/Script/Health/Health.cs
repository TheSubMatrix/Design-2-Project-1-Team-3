using System;
using System.Threading;
using System.Threading.Tasks;
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
    [SerializeField] bool m_invulnerabilityAfterDamage = false;
    [SerializeField] float m_invulnerabilityTime = 1;
    Task m_invulnerabilityTask;
    CancellationTokenSource m_cancellationTokenSource;
    public bool IsAlive => CurrentHealth > 0;
    public bool IsInvulnerable { get; private set; }
    public void Awake()
    {
        CurrentHealth = MaxHealth;
        m_cancellationTokenSource = new CancellationTokenSource();
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
            return;
        }
        if(!m_invulnerabilityAfterDamage) return;
        try
        {
            using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken, m_cancellationTokenSource.Token);
            m_invulnerabilityTask = InvulnerableFor(m_invulnerabilityTime, cts.Token);
        }
        catch
        {
            // ignored
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
    public void MakeInvulnerable()
    {
        IsInvulnerable = true;
    }

    public void MakeVulnerable()
    {
        IsInvulnerable = false;
    }
    async Task InvulnerableFor(float time, CancellationToken token)
    {
        IsInvulnerable = true;
        await Awaitable.WaitForSecondsAsync(time, token);
        IsInvulnerable = false;
    }
    void OnDestroy()
    {
        m_cancellationTokenSource.Cancel();
        m_cancellationTokenSource.Dispose();
    }
}
