using UnityEngine;

public interface IShockable
{
    IDamageable CurrentDamageable { get; }

    void Shock(uint damage)
    {
        CurrentDamageable?.Damage(damage);
    }
}
