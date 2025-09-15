using System;
using System.Linq;
using CustomNamespace.Extensions;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NewWeaponItem : BasePickup
{
    [SerializeField] float m_rotationSpeed = 100;
    [SerializeField] float m_bobbingSpeed = 1;
    [SerializeField] float m_bobbingAmount = 0.25f;
    [SerializeField]SpellSettingsSO m_staffSpellToGive;
    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.gameObject.GetComponentInChildren<CameraControl>()?.m_cam.GetComponentInChildren<Staff>();
        if (staff is null)
        {
            return;
        }
        Staff.SpellSlot[] matchingSlots = staff.SpellSlots
            .Where(slot => slot.Spell.SpellName == m_staffSpellToGive.SpellInstance.SpellName)
            .ToArray();

        if (matchingSlots.Length <= 0)
        {
            staff.SpellSlots.Add(new Staff.SpellSlot(m_staffSpellToGive.SpellInstance.CopyWithAllValues()));
            staff.OnStaffSpellChange.Invoke(staff.GetSpellDataForCurrentSlot());
            Destroy(gameObject);
        }
        else
        {
            foreach (Staff.SpellSlot slot in matchingSlots)
            {
                if (!(slot.RemainingUseCount < slot.Spell.UseCount)) continue;
                slot.RemainingUseCount = slot.Spell.UseCount;
                staff.OnStaffSpellChange.Invoke(staff.GetSpellDataForCurrentSlot());
                Destroy(gameObject);
                return;
            }
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * m_bobbingSpeed) * Time.deltaTime * m_bobbingAmount), transform.position.z);
    }
}
