using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [SerializeField] Material m_staffBallMaterial;
    [field:SerializeField] public List<StaffSpellSO> Attacks{ get; set;}
    int m_attackIndex;
    public void Attack()
    {
        Attacks[m_attackIndex]?.ExecuteAttack(m_projectileFirePoint.position, transform.forward, m_projectileFirePoint.rotation);
    }

    void Awake()
    {
        foreach (StaffSpellSO attack in Attacks)
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

        if (Attacks.Count <= 0 || Input.GetAxis("Mouse ScrollWheel") == 0f) return;
        m_attackIndex = (m_attackIndex + (Input.GetAxis("Mouse ScrollWheel") > 0f ? 1 : -1) + Attacks.Count) % Attacks.Count;
        m_staffBallMaterial.color = Attacks[m_attackIndex].SpellBallColor;

    }
}
