using System;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class NewWeaponItem : MonoBehaviour
{
    StaffSpellSO m_staffSpellToGive;

    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.GetComponentInChildren<Staff>();
        if(staff is null) return;
        if(!staff.Attacks.Contains(m_staffSpellToGive))
        {
            staff.Attacks.Add(m_staffSpellToGive);
        }
    }
}
