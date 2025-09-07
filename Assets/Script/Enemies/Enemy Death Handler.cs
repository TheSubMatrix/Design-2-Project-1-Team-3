using Unity.Behavior;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    static readonly int Dead = Animator.StringToHash("Dead");
    [SerializeField] BehaviorGraphAgent m_agent;
    [SerializeField] Animator m_animator;
    void Start()
    {
        m_agent ??= GetComponent<BehaviorGraphAgent>();
        m_animator ??= GetComponent<Animator>();
    }

    public void OnDeath()
    {
        m_agent.End();
        m_animator.SetBool(Dead, true);
    }
}
