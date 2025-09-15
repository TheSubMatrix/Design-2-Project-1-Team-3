using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using AudioSystem;
using CustomNamespace.Extensions;
using UnityEngine.Events;

public class Staff : MonoBehaviour
{
    [SerializeField]
    public UnityEvent <SpellData> OnStaffSpellChange = new();
    public Transform m_firePoint;
    [SerializeField] Renderer m_staffBallRenderer;
    public List<SpellSlot> SpellSlots { get; set; } = new();
    [SerializeField] List<SpellSettingsSO> m_spellSettings = new();
    [Serializable]
    public class SpellSlot
    {
        public SpellSlot(StaffSpell spell)
        {
            Spell = spell;
            RemainingUseCount = spell.UseCount;
        }
        public StaffSpell Spell;
        public uint? RemainingUseCount;
    }
    int m_attackIndex;
    public struct SpellData 
    {
        public SpellData(string selectedSpell, uint? selectedSpellUses, Color spellColor, Sprite spellSprite)
        { 
            SelectedSpell = selectedSpell;
            SelectedSpellUses = selectedSpellUses;
            SpellColor = spellColor;
            SpellSprite = spellSprite;
        }
        public string SelectedSpell { get; set; }
        public uint? SelectedSpellUses { get; set; }
        public Color SpellColor { get; set; }
        public Sprite SpellSprite { get; set; }
    }
    public void Attack()
    {
        SpellSlots[m_attackIndex].Spell?.ExecuteAttack(m_firePoint.position, m_firePoint.forward, m_firePoint.rotation);
        SoundManager.Instance.CreateSound().WithSoundData(SpellSlots[m_attackIndex].Spell?.CastSound).WithPosition(transform.position).WithRandomPitch().Play();
        if (SpellSlots[m_attackIndex].RemainingUseCount is not > 0) return;
        SpellSlots[m_attackIndex].RemainingUseCount--;
        if (SpellSlots[m_attackIndex].RemainingUseCount is not <= 0) return;
        SpellSlots.RemoveAt(m_attackIndex);
        if (m_attackIndex > SpellSlots.Count - 1)
        {
            m_attackIndex = SpellSlots.Count - 1;
        }
        if(SpellSlots[m_attackIndex].Spell is not null)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;
    }

    void Awake()
    {
        foreach (SpellSettingsSO spellSettings in m_spellSettings)
        {
            StaffSpell spell = spellSettings.SpellInstance.CopyWithAllValues();
            SpellSlots.Add(new SpellSlot(spell));
        }
        if (SpellSlots[m_attackIndex].Spell is not null)
        {
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;
        }
        foreach (SpellSlot attack in SpellSlots)
        {
            attack.Spell?.Initialize(this);
            // Ensure per-staff remaining uses are initialized because Unity doesn't run the ctor
            if (attack.RemainingUseCount is null && attack.Spell != null)
            {
                attack.RemainingUseCount = attack.Spell.UseCount;
            }
        }
        OnStaffSpellChange?.Invoke(GetSpellDataForCurrentSlot());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
            OnStaffSpellChange?.Invoke(GetSpellDataForCurrentSlot());
        }

        if (SpellSlots.Count <= 0 || Input.GetAxis("Mouse ScrollWheel") == 0f) return;
        m_attackIndex = (m_attackIndex + (Input.GetAxis("Mouse ScrollWheel") > 0f ? 1 : -1) + SpellSlots.Count) % SpellSlots.Count;
        OnStaffSpellChange?.Invoke(GetSpellDataForCurrentSlot());
        if (SpellSlots[m_attackIndex].Spell is not null)
            m_staffBallRenderer.material.color = SpellSlots[m_attackIndex].Spell.SpellBallColor;

    }

    public SpellData GetSpellDataForCurrentSlot()
    {
        return new SpellData(SpellSlots[m_attackIndex].Spell.SpellName,
            SpellSlots[m_attackIndex].RemainingUseCount, SpellSlots[m_attackIndex].Spell.SpellBallColor,
            SpellSlots[m_attackIndex].Spell.SpellSprite);
    }
}
