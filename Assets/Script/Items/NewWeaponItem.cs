using System;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class NewWeaponItem : MonoBehaviour
{
    StaffSpellSO m_staffSpellToGive;

    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.GetComponentInChildren<Staff>();
        if(staff is null) return;
        if(staff.Attacks.Where((spellAndAttacksRemaining) => spellAndAttacksRemaining.Spell == m_staffSpellToGive).ToArray().Length <= 0)
        {
            staff.Attacks.Add(new Staff.SpellAndAttacksRemaining(m_staffSpellToGive));
        }
    }
}
