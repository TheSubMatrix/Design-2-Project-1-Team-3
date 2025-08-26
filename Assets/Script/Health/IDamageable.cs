using UnityEngine;

public interface IDamageable
{
    void Damage(uint damage);
    public MonoBehaviour CurrentMonoBehaviour { get; }
}
