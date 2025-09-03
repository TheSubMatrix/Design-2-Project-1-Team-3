using UnityEngine;
using System;
using CustomNamespace.Extensions;
using UnityEngine.VFX;

[CreateAssetMenu(menuName = "Scriptable Objects/Spells/Thunder Spell", fileName = "New Thunder Spell"), Serializable]
public class ThunderSpell : StaffSpellSO
{
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField] VisualEffectAsset m_spellEffect;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] bool m_hasLimitedUses;
    [OnSelectionRender(nameof(m_hasLimitedUses),true), SerializeField]
    uint m_maxUses;
    public override uint? UseCount => m_hasLimitedUses ? m_maxUses : null;
    
    public override void Initialize()
    {
        
    }
    public override void ExecuteAttack(GameObject attackingObject, Vector3 position, Vector3 direction, Quaternion rotation)
    {
        if (!Physics.Raycast(position, direction, out RaycastHit hit)) return;
        ThunderSpellVFX thunderSpellVFX = attackingObject.GetComponent<ThunderSpellVFX>();
        if (thunderSpellVFX)
        {
            thunderSpellVFX.PlayVFX(new []{ new ThunderSpellVFX.BoltPosition(hit.point, attackingObject.transform.position)});
        }
        IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
        damageable?.Damage(10);
    }
}
