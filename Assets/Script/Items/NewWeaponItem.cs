using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NewWeaponItem : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed;
    [SerializeField]StaffSpellSO m_staffSpellToGive;

    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.gameObject.GetComponentInChildren<CameraControl>()?.m_cam.GetComponentInChildren<Staff>();
        if (staff is null)
        {
            Debug.Log("No staff found");
            return;
        }
        Staff.SpellSlot[] matchingSlots = staff.SpellSlots
            .Where(slot => slot.Spell == m_staffSpellToGive)
            .ToArray();

        if (matchingSlots.Length <= 0)
        {
            staff.SpellSlots.Add(new Staff.SpellSlot(m_staffSpellToGive));
            Destroy(gameObject);
        }
        else
        {
            foreach (Staff.SpellSlot slot in matchingSlots)
            {
                if (!(slot.RemainingUseCount < slot.Spell.UseCount)) continue;
                slot.RemainingUseCount = slot.Spell.UseCount;
                Destroy(gameObject);
                return;
            }
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
}
