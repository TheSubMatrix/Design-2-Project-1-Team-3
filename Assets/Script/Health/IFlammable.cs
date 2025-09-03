using System.Collections;
using UnityEngine;

public interface IFlammable
{
    public MonoBehaviour CurrentMonoBehaviour { get; }
    public IDamageable CurrentDamageable { get; }
    delegate void DamageDelegate(uint damage);
    public void OnSetFire(float duration, uint damage)
    {
        CurrentMonoBehaviour?.StartCoroutine(DamageOverTimeAsync(duration, damage, CurrentDamageable.Damage));
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
