using System;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [SerializeField] List<StaffAttack> m_attacks;
    public void Attack()
    {
        m_attacks[0]?.ExecuteAttack(m_projectileFirePoint.position, m_projectileFirePoint.rotation);
    }

    void Awake()
    {
        foreach (StaffAttack attack in m_attacks)
        {
            attack.Initialize();
        }
    }
}
