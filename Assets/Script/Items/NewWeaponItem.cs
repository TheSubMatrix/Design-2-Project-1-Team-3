using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NewWeaponItem : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed;
    [SerializeField]StaffSpellSO m_staffSpellToGive;
    float m_yPos;
    void OnTriggerEnter(Collider other)
    {
        Staff staff = other.gameObject.GetComponentInChildren<CameraControl>()?.m_cam.GetComponentInChildren<Staff>();
        if (staff is null)
        {
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

    void Start()
    {
        m_yPos = transform.position.y;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, m_yPos + (Mathf.Sin(Time.time) * 0.25f), transform.position.z);
    }
}
