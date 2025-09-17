using System;
using UnityEngine;

public class HealthPickup : BasePickup
{
    [SerializeField] uint m_healAmount = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        IHealable healable = other.gameObject.GetComponent<IHealable>();
        if (healable is null || !healable.IsHealable) return;
        healable.Heal(m_healAmount);
        Collect();
    }

}
