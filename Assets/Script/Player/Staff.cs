using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using AudioSystem;
using UnityEngine.Events;

public class Staff : MonoBehaviour
{
    [SerializeField]
    UnityEvent <SpellData> OnStaffSpellChange = new UnityEvent<SpellData>();
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
    public struct SpellData 
    {
        public SpellData(string selectedSpell, uint? selectedSpellUses, Color spellColor)
        { 
            SelectedSpell = selectedSpell;
            SelectedSpellUses = selectedSpellUses;
            SpellColor = spellColor;
        }
        public string SelectedSpell { get; set; }
        public uint? SelectedSpellUses { get; set; }
        public Color SpellColor { get; set; }
    }
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
            OnStaffSpellChange.Invoke(new SpellData(SpellSlots[m_attackIndex].Spell.SpellName, SpellSlots[m_attackIndex].RemainingUseCount, SpellSlots[m_attackIndex].Spell.SpellBallColor));
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
        OnStaffSpellChange.Invoke(new SpellData(SpellSlots[m_attackIndex].Spell.SpellName, SpellSlots[m_attackIndex].RemainingUseCount, SpellSlots[m_attackIndex].Spell.SpellBallColor));
        if (SpellSlots[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;

    }
}
