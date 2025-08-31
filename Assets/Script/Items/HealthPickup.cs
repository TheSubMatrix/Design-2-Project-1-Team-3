using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] uint m_healAmount = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        other.gameObject.GetComponent<IHealable>()?.Heal(m_healAmount);
        Destroy(gameObject);
    }
}
