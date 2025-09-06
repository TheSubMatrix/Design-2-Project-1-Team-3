using UnityEngine;
using System;
using CustomNamespace.Extensions;
using UnityEngine.VFX;
//using TMPro;

[CreateAssetMenu(menuName = "Scriptable Objects/Spells/Thunder Spell", fileName = "New Thunder Spell"), Serializable]
public class ThunderSpell : StaffSpellSO
{
    //public TMP_Text name;
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField] LayerMask m_layerMask;
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
        if (!Physics.Raycast(position, direction, out RaycastHit hit, Mathf.Infinity, m_layerMask, QueryTriggerInteraction.Ignore)) return;
        ThunderSpellVFX thunderSpellVFX = attackingObject.GetComponent<ThunderSpellVFX>();
        if (thunderSpellVFX)
        {
            thunderSpellVFX.PlayVFX(new []{ new ThunderSpellVFX.BoltPosition(hit.point, attackingObject.transform.position)});
        }
        IShockable damageable = hit.collider.gameObject.GetComponent<IShockable>();
        damageable?.Shock(10);
    }
}
