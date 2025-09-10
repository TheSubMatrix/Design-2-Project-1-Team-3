using UnityEngine;

public class DeathPit : MonoBehaviour
{
    [SerializeField] uint m_attackDamage;
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
        Debug.Log(other.gameObject.GetComponent<IDamageable>());

    }
}
