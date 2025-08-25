using System.Threading;
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
    public UnityEvent OnBecameInvulnerableEvent = new();
    public UnityEvent OnBecameVulnerableEvent = new();
    [SerializeField] bool m_invulnerabilityAfterDamage;
    [SerializeField] float m_invulnerabilityTime = 1;
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
        using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken, m_cancellationTokenSource.Token);
        InvulnerableFor(m_invulnerabilityTime, cts.Token);
    }

    MonoBehaviour IDamageable.CurrentMonoBehaviour => this;

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
        if(!m_cancellationTokenSource.IsCancellationRequested) m_cancellationTokenSource.Cancel();
        OnBecameInvulnerableEvent.Invoke();
    }

    public void MakeVulnerable()
    {
        IsInvulnerable = false;
        if(!m_cancellationTokenSource.IsCancellationRequested) m_cancellationTokenSource.Cancel();
        OnBecameVulnerableEvent.Invoke();
    }
    async void InvulnerableFor(float time, CancellationToken token)
    {
        try
        {
            IsInvulnerable = true;
            await Awaitable.WaitForSecondsAsync(time, token);
            IsInvulnerable = false;
        }catch
        {
            // ignored
        }
    }
    void OnDestroy()
    {
        m_cancellationTokenSource?.Cancel();
        m_cancellationTokenSource?.Dispose();
    }
}
