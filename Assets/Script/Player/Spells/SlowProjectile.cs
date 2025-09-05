using UnityEngine;
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
        if(gameObject.activeSelf) Pool?.Release(this);
    }

    void FixedUpdate()
    {
        if(ProjectileRigidbody.linearVelocity.magnitude <= 0) return;
        transform.rotation = Quaternion.LookRotation(ProjectileRigidbody.linearVelocity);
    }
}
