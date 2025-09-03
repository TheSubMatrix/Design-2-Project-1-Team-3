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
            IFlammable flammable = contact.otherCollider.gameObject.GetComponent<IFlammable>();
            flammable?.OnSetFire(m_duration, Damage);
        }
        if (!gameObject.activeSelf) return;
        Pool?.Release(this);
        
    }
    
}
