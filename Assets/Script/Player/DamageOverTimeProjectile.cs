using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
/// <summary>
/// The base class for any object fired from a <see cref="Weapon"/>. This class is designed to use an <see cref="ObjectPool{T}"/> no minimize the creation and destruction of <see cref="GameObject"/> runtime
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DamageOverTimeProjectile : Projectile
{
    [SerializeField] protected float m_duration = 10;
    delegate void DamageDelegate(uint damage);
    protected override void OnCollisionEnter(Collision collision)
    {
        DamageDelegate onDamage = collision.contacts.Aggregate<ContactPoint, DamageDelegate>(null, (current, contact) => current + contact.otherCollider.gameObject.GetComponent<IDamageable>().Damage);
        StartCoroutine(DamageOverTimeAsync(m_duration, Damage, onDamage)); 
        Pool?.Release(this);
    }

    IEnumerator DamageOverTimeAsync(float totalDuration, uint totalDamage, DamageDelegate onDamage)
    {
        float timeBetweenDamage = totalDuration / totalDamage;
        float timeElapsed = 0;
        float timeSinceLastDamage = 0;
        while (timeElapsed < totalDuration)
        {
            timeElapsed += Time.deltaTime;
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= timeBetweenDamage)
            {
                onDamage.Invoke(1);
            }
            yield return null;
        }
    }
}
