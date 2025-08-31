using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using AudioSystem;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform m_projectileFirePoint;
    [SerializeField] Renderer m_staffBallRenderer;
    [field:SerializeField] public List<SpellAndAttacksRemaining> Attacks{ get; set;}

    [Serializable]
    public class SpellAndAttacksRemaining
    {
        public SpellAndAttacksRemaining(StaffSpellSO spell)
        {
            Spell = spell;
            UseCount = spell.UseCount;
        }
        public StaffSpellSO Spell;
        [ReadOnly]public uint? UseCount;
    }
    int m_attackIndex;
    public void Attack()
    {
        Attacks[m_attackIndex].Spell?.ExecuteAttack(m_projectileFirePoint.position, transform.forward, m_projectileFirePoint.rotation);
        SoundManager.Instance.CreateSound().WithSoundData(Attacks[m_attackIndex].Spell?.CastSound).WithPosition(transform.position).WithRandomPitch().Play();
        if (Attacks[m_attackIndex].UseCount is not > 0) return;
        Attacks[m_attackIndex].UseCount--;
        if (Attacks[m_attackIndex].UseCount is not <= 0) return;
        Attacks.RemoveAt(m_attackIndex);
        if (m_attackIndex > Attacks.Count - 1)
        {
            m_attackIndex = Attacks.Count - 1;
        }
        if(Attacks[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = Attacks[m_attackIndex].Spell.SpellBallColor;
    }

    void Awake()
    {
        if(Attacks[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = Attacks[m_attackIndex].Spell.SpellBallColor;
        foreach (SpellAndAttacksRemaining attack in Attacks)
        {
            attack.Spell?.Initialize();
            // Ensure per-staff remaining uses are initialized because Unity doesn't run the ctor
            if (attack.UseCount is null && attack.Spell != null)
            {
                attack.UseCount = attack.Spell.UseCount;
            }
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
        if(Attacks[m_attackIndex].Spell)
            m_staffBallRenderer.material.color = Attacks[m_attackIndex].Spell.SpellBallColor;

    }
}
