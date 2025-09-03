using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using AudioSystem;

public class Staff : MonoBehaviour
{
    [FormerlySerializedAs("m_projectileFirePoint")] [SerializeField] Transform m_firePoint;
    [SerializeField] Renderer m_staffBallRenderer;
    [field: FormerlySerializedAs("<Attacks>k__BackingField")] [field:SerializeField] public List<SpellSlot> SpellSlots{ get; set;}

    [Serializable]
    public class SpellSlot
    {
        public SpellSlot(StaffSpellSO spell)
        {
            Spell = spell;
            RemainingUseCount = spell.UseCount;
        }
        public StaffSpellSO Spell;
        public uint? RemainingUseCount;
    }
    int m_attackIndex;
    public void Attack()
    {
        SpellSlots[m_attackIndex].Spell?.ExecuteAttack(m_firePoint.gameObject, m_firePoint.position, transform.forward, m_firePoint.rotation);
        SoundManager.Instance.CreateSound().WithSoundData(SpellSlots[m_attackIndex].Spell?.CastSound).WithPosition(transform.position).WithRandomPitch().Play();
        if (SpellSlots[m_attackIndex].RemainingUseCount is not > 0) return;
        SpellSlots[m_attackIndex].RemainingUseCount--;
        if (SpellSlots[m_attackIndex].RemainingUseCount is not <= 0) return;
        SpellSlots.RemoveAt(m_attackIndex);
        if (m_attackIndex > SpellSlots.Count - 1)
        {
            m_attackIndex = SpellSlots.Count - 1;
        }
        if(SpellSlots[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;
    }

    void Awake()
    {
        if(SpellSlots[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;
        foreach (SpellSlot attack in SpellSlots)
        {
            attack.Spell?.Initialize();
            // Ensure per-staff remaining uses are initialized because Unity doesn't run the ctor
            if (attack.RemainingUseCount is null && attack.Spell != null)
            {
                attack.RemainingUseCount = attack.Spell.UseCount;
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (SpellSlots.Count <= 0 || Input.GetAxis("Mouse ScrollWheel") == 0f) return;
        m_attackIndex = (m_attackIndex + (Input.GetAxis("Mouse ScrollWheel") > 0f ? 1 : -1) + SpellSlots.Count) % SpellSlots.Count;
        if(SpellSlots[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;

    }
}
