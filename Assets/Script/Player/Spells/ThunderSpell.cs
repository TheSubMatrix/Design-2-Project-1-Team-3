using UnityEngine;
using UnityEngine.VFX;

public class ThunderSpell : StaffSpell
{
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField] LayerMask m_layerMask;
    [SerializeField] VisualEffectAsset m_spellEffect;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] bool m_hasLimitedUses;
    [SerializeField] uint m_maxUses;
    VFXEventAttribute m_boltDataToSend;
    VisualEffectAsset m_vfxAsset;
    VisualEffect m_vfxInstance;
    
    public struct  BoltPosition
    {
        public Vector3 StartPosition { get; }
        public Vector3 EndPosition { get; }
        public BoltPosition(Vector3 endPosition, Vector3 startPosition)
        {
            EndPosition = endPosition;
            StartPosition = startPosition;
        }
    }
    void PlayVFX(BoltPosition[] boltPositions)
    {
        foreach (BoltPosition positions in boltPositions)
        {
            m_boltDataToSend.SetVector3("StartPosition", positions.StartPosition);
            m_boltDataToSend.SetVector3("endPoint", positions.EndPosition);
            m_vfxInstance?.SendEvent("Fire", m_boltDataToSend);
        }
    }

    public override uint? UseCount => m_hasLimitedUses ? m_maxUses : null;
    
    public override void Initialize(Staff owner)
    {
        base.Initialize(owner);
        VisualEffect[] thunderSpellVFX = SpellOwner.GetComponentsInChildren<VisualEffect>();
        m_vfxInstance = thunderSpellVFX[0];
        m_boltDataToSend = m_vfxInstance.CreateVFXEventAttribute();
    }

    public override void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation)
    {
        if (!Physics.Raycast(position, direction, out RaycastHit hit, Mathf.Infinity, m_layerMask, QueryTriggerInteraction.Ignore)) return;
        PlayVFX(new []{new BoltPosition(hit.point, SpellOwner.m_firePoint.position)});
        IShockable damageable = hit.collider.gameObject.GetComponent<IShockable>();
        damageable?.Shock(10);
    }

    ~ThunderSpell()
    {
        m_boltDataToSend.Dispose();
    }
}
