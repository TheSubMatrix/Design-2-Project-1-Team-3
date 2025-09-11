using UnityEngine;

public class FireBar : MonoBehaviour
{
    [SerializeField] uint m_attackDamage;
    private float fanSpeed = 75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, fanSpeed * Time.deltaTime, 0);
    }
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
        //Debug.Log(other.gameObject.GetComponent<IDamageable>());

    }
}
