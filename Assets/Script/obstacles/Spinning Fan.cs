using System;
using System.Collections;
using UnityEngine;

public class SpinningFan : MonoBehaviour, ISlowable
{
    [SerializeField] uint m_attackDamage;
    [SerializeField] float m_fanSpeed = 400f;
    bool m_frozen;
    Coroutine m_slowCoroutine;
    public void Slow(float slowAmount, float duration)
    {
        StartCoroutine(SlowForTimeAsync(slowAmount, duration));
    }
    
    public void Update()
    {
        if(m_frozen) return;   
        transform.Rotate(m_fanSpeed * Time.deltaTime, 0, 0);
    }

    IEnumerator SlowForTimeAsync(float slowPercent, float duration)
    {
        m_frozen = true;
        yield return new WaitForSeconds(duration);
        m_frozen = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
    }
}
