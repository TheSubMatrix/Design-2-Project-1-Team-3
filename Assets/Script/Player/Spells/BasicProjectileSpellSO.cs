using System;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "Scriptable Objects/Spells/Basic Projectile Spell", fileName = "New Basic Projectile Spell"), Serializable]
public class BasicProjectileSpellSO : StaffSpellSO
{
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] GameObject m_projectilePrefab;
    ObjectPool<Projectile> m_projectilePool;
    [SerializeField] bool m_hasLimitedUses;
    [OnSelectionRender(nameof(m_hasLimitedUses),true), SerializeField]
    uint m_maxUses;
    public override uint? UseCount => m_hasLimitedUses ? m_maxUses : null;
    public override void Initialize()
    {
        if (m_projectilePrefab?.GetComponent<Projectile>() is null)
        {
            Debug.LogError("Projectile prefab must have a Projectile component");
            return;  
        }
        m_projectilePool = new ObjectPool<Projectile>
        (
            () => Projectile.OnSpawn(m_projectilePrefab),
            projectile => projectile.OnPull(m_projectilePool),
            projectile => projectile.OnRelease(),
            projectile => Destroy(projectile.gameObject)
        );
        
    }
    public override void ExecuteAttack(Vector3 position,Vector3 direction, Quaternion rotation)
    {
        Projectile projectileFromPool = m_projectilePool.Get();
        projectileFromPool.OnInitialize(position,rotation);
        projectileFromPool.OnFire(direction * 10);
    }
}
