using System;
using System.Collections;
using Unity.Behavior;
using Unity.Behavior.GraphFramework;
using UnityEngine;

public class EnemyMovementSpeedBehaviorModifier : MonoBehaviour, ISlowable
{
    [SerializeField]BehaviorGraphAgent m_agent;
    Coroutine m_slowCoroutine;
    BlackboardVariable<float> m_speedVariable;
    float m_defaultSpeed;
    [SerializeField] string m_speedVariableName = "Speed";
    void Start()
    {
        m_agent ??= GetComponent<BehaviorGraphAgent>();
        if (!m_agent.GetVariable(m_speedVariableName, out m_speedVariable)) return;
        m_defaultSpeed = m_speedVariable.Value;
    }

    public void Slow(float slowPercent, float duration)
    {
        if (m_slowCoroutine != null) StopCoroutine(m_slowCoroutine);
        m_slowCoroutine = StartCoroutine(SlowForTimeAsync(slowPercent, duration));
    }

    IEnumerator SlowForTimeAsync(float slowPercent, float duration)
    {
        m_speedVariable.Value =  m_defaultSpeed * slowPercent;
        yield return new WaitForSeconds(duration);
        m_speedVariable.Value =  m_defaultSpeed;
    }
}
