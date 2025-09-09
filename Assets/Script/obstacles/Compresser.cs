using UnityEngine;

public class Compresser : MonoBehaviour
{
    [SerializeField] uint m_attackDamage;
    public float speed = 10f; // Speed of the movement
    public float height = 10f; // Height of the movement

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;

        // Update the object's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IDamageable>()?.Damage(m_attackDamage);
    }
}
