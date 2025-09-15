using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class EnemyMeleeAttack : EnemyAttack
{
    [SerializeField] Collider m_attackCollider;
    [SerializeField] float m_attackDuration;
    [SerializeField] uint m_attackDamage;
    bool m_isAttacking;
    void Awake()
    {
        m_attackCollider ??= GetComponent<Collider>();
        m_attackCollider.enabled = false;
        m_attackCollider.isTrigger = true;
    }

    public override void OnDeath()
    {
        StopCoroutine(AttackRoutineAsync());
        m_attackCollider.enabled = false;
        m_isAttacking = false;
    }
    public override void Attack()
    {
        if(m_isAttacking) return;
        StartCoroutine(AttackRoutineAsync());
    }

    IEnumerator AttackRoutineAsync()
    {
        m_isAttacking = true;
        m_attackCollider.enabled = true;
        yield return new WaitForSeconds(m_attackDuration);
        m_attackCollider.enabled = false;
        m_isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
    }
}
