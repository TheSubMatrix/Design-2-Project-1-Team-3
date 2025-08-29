using System;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(menuName = "Scriptable Objects/Attacks/Basic Projectile Attack", fileName = "New Basic Projectile Attack"), Serializable]
public class BasicProjectileSpellSO : StaffSpellSO
{
    public override Color SpellBallColor => m_spellBallColor;
    [SerializeField][ColorUsage(false, true)] Color m_spellBallColor;
    [SerializeField] GameObject m_projectilePrefab;
    ObjectPool<Projectile> m_projectilePool;
    public override void Initialize()
    {
        if (m_projectilePrefab?.GetComponent<Projectile>() is null)
        {
            Debug.LogError("Projectile prefab must have a Projectile component");
            return;  
        }
        m_projectilePool = new ObjectPool<Projectile>
        (
            () => Instantiate(m_projectilePrefab).GetComponent<Projectile>(), 
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
