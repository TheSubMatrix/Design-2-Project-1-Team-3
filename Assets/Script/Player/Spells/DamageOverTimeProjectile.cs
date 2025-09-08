using AudioSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DamageOverTimeProjectile : Projectile
{
    [SerializeField] SoundData m_impactSound;
    [SerializeField] protected float m_duration = 10;
    protected override void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        { 
            IFlammable flammable = contact.otherCollider.gameObject.GetComponent<IFlammable>();
            flammable?.OnSetFire(m_duration, Damage);
        }
        SoundManager.Instance.CreateSound().WithSoundData(m_impactSound).WithPosition(transform.position).WithRandomPitch().Play();
        if (!gameObject.activeSelf) return;
        Pool?.Release(this);
    }
    
}
