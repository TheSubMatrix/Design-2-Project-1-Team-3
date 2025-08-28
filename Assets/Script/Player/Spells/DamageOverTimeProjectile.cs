using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
[RequireComponent(typeof(Rigidbody))]
public class DamageOverTimeProjectile : Projectile
{
    [SerializeField] protected float m_duration = 10;
    delegate void DamageDelegate(uint damage);
    protected override void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        { 
            IDamageable damageable = contact.otherCollider.gameObject.GetComponent<IDamageable>();
            damageable?.CurrentMonoBehaviour.StartCoroutine(DamageOverTimeAsync(m_duration, Damage,contact.otherCollider.gameObject.GetComponent<IDamageable>().Damage));
        }

        if (gameObject.activeSelf)
        {
            Pool?.Release(this);
        }
    }

    IEnumerator DamageOverTimeAsync(float totalDuration, uint totalDamage, DamageDelegate onDamage)
    {
        if (onDamage == null || totalDamage == 0)
            yield break;

        if (totalDuration <= 0f)
        {
            onDamage(totalDamage);
            yield break;
        }
        
        float interval = totalDuration / totalDamage;
        for (uint i = 0; i < totalDamage; i++)
        {
            yield return new WaitForSeconds(interval);
            onDamage(1);
        }
    }
}
