using System;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [SerializeField] StaffAttack m_attack;
    void Attack()
    {
        m_attack.ExecuteAttack(m_projectileFirePoint.position, m_projectileFirePoint.rotation);
    }

    void Awake()
    {
        m_attack.Initialize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
}
