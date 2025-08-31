using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
[RequireComponent(typeof(Rigidbody))]
public class ThunderProjectile : Projectile
{
    [SerializeField] protected float m_duration = 2;
    [SerializeField] protected float m_stunAmount = 1f;
    protected override void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        {
            IDamageable damageable = contact.otherCollider.gameObject.GetComponent<IDamageable>();
            ISlowable slowable = contact.otherCollider.gameObject.GetComponent<ISlowable>();
            slowable?.Slow(m_stunAmount, m_duration);
            damageable?.Damage(Damage);
        }
        if (gameObject.activeSelf) Pool?.Release(this);
    }

}
