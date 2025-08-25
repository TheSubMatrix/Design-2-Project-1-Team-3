using System;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class NewWeaponItem : MonoBehaviour
{
    StaffAttackSO m_staffAttackToGive;

    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.GetComponentInChildren<Staff>();
        if(staff is null) return;
        if(!staff.Attacks.Contains(m_staffAttackToGive))
        {
            staff.Attacks.Add(m_staffAttackToGive);
        }
    }
}
