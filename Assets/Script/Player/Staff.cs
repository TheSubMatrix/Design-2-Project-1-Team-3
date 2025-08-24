using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [SerializeField] List<StaffAttack> m_attacks;
    int m_attackIndex;
    public void Attack()
    {
        m_attacks[m_attackIndex]?.ExecuteAttack(m_projectileFirePoint.position, transform.forward, m_projectileFirePoint.rotation);
    }

    void Awake()
    {
        foreach (StaffAttack attack in m_attacks)
        {
            attack.Initialize();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        if (m_attacks.Count > 0 && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            m_attackIndex = (m_attackIndex + (Input.GetAxis("Mouse ScrollWheel") > 0f ? 1 : -1) + m_attacks.Count) % m_attacks.Count;
        }

    }
}
