using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [field:SerializeField] public List<StaffAttackSO> Attacks{ get; set;}
    int m_attackIndex;
    public void Attack()
    {
        Attacks[m_attackIndex]?.ExecuteAttack(m_projectileFirePoint.position, transform.forward, m_projectileFirePoint.rotation);
    }

    void Awake()
    {
        foreach (StaffAttackSO attack in Attacks)
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
        if (Attacks.Count > 0 && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            m_attackIndex = (m_attackIndex + (Input.GetAxis("Mouse ScrollWheel") > 0f ? 1 : -1) + Attacks.Count) % Attacks.Count;
        }

    }
}
