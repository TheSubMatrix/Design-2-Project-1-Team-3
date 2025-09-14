using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

[Serializable]
public class BasicProjectileSpell : StaffSpell
{
    [SerializeField] float m_projectileForce = 10;
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] GameObject m_projectilePrefab;
    ObjectPool<Projectile> m_projectilePool;
    [SerializeField] bool m_hasLimitedUses;
    [SerializeField]
    uint m_maxUses;
    public override uint? UseCount => m_hasLimitedUses ? m_maxUses : null;
    public override void Initialize(Staff owner)
    {
        base.Initialize(owner);
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
            projectile => Object.Destroy(projectile.gameObject)
        );
        
    }
    public override void ExecuteAttack(Vector3 position,Vector3 direction, Quaternion rotation)
    {
        Projectile projectileFromPool = m_projectilePool.Get();
        projectileFromPool.OnInitialize(position,rotation);
        projectileFromPool.OnFire(direction * m_projectileForce);
    }
}
