using System;
using System.Collections;
using UnityEngine;

public class SpinningFan : MonoBehaviour, ISlowable
{
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
        transform.Rotate(0, m_fanSpeed * Time.deltaTime, 0);
    }

    IEnumerator SlowForTimeAsync(float slowPercent, float duration)
    {
        m_frozen = true;
        yield return new WaitForSeconds(duration);
        m_frozen = false;
    }
}
