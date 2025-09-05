using UnityEngine;

public class rotateAxes : MonoBehaviour
{
    [SerializeField] uint m_attackDamage;
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
        Debug.Log(other.gameObject.GetComponent<IDamageable>());
    }
}
