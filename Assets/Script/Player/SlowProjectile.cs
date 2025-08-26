using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
[RequireComponent(typeof(Rigidbody))]
public class SlowProjectile : Projectile
{
    [SerializeField] protected float m_duration = 10;
    [SerializeField] protected float m_slowAmount = 0.5f;
    protected override void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        { 
            IDamageable damageable = contact.otherCollider.gameObject.GetComponent<IDamageable>();
            ISlowable slowable = contact.otherCollider.gameObject.GetComponent<ISlowable>();
            slowable?.Slow(m_slowAmount, m_duration);
            damageable?.Damage(Damage);
        }
        Pool?.Release(this);
    }
    
}
