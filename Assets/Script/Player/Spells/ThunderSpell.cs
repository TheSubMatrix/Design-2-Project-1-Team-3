using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Scriptable Objects/Spells/Thunder Spell", fileName = "New Thunder Spell"), Serializable]
public class ThunderSpell : StaffSpellSO
{
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] bool m_hasLimitedUses;
    [OnSelectionRender(nameof(m_hasLimitedUses),true), SerializeField]
    uint m_maxUses;
    public override uint? UseCount => m_hasLimitedUses ? m_maxUses : null;
    
    public override void Initialize()
    {
        
    }
    public override void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.Damage(10);
        }
    }
}
