using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed = 100;
    [SerializeField] float m_bobbingSpeed = 1;
    [SerializeField] float m_bobbingAmount = 0.25f;
    [SerializeField] uint m_healAmount = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        IHealable healable = other.gameObject.GetComponent<IHealable>();
        if (healable is null || !healable.IsHealable) return;
        healable.Heal(m_healAmount);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * m_bobbingSpeed) * Time.deltaTime * m_bobbingAmount), transform.position.z);
    }
}
