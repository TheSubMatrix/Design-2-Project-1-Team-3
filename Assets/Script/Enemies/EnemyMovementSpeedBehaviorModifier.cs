using System;
using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class EnemyMovementSpeedBehaviorModifier : MonoBehaviour, ISlowable
{
    [SerializeField]BehaviorGraphAgent m_agent;
    BlackboardVariable<float> m_currentSpeed;
    void Awake()
    {
        m_agent ??= GetComponent<BehaviorGraphAgent>();
        m_agent.BlackboardReference.GetVariable("Speed", out BlackboardVariable<float> currentSpeed);
        m_currentSpeed = currentSpeed;
    }

    public void Slow(float slowAmount, float duration)
    {
        StartCoroutine(SlowForTimeAsync(slowAmount, duration));
    }

    IEnumerator SlowForTimeAsync(float slowAmount, float duration)
    {
        float speedBeforeSlow = m_currentSpeed.Value;
        m_currentSpeed.Value = speedBeforeSlow * slowAmount;
        yield return new WaitForSeconds(duration);
        m_currentSpeed.Value = speedBeforeSlow;
    }
}
