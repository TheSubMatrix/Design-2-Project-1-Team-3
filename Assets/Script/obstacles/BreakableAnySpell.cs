using UnityEngine;

public class BreakableAnySpell : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("Ice") || other.gameObject.CompareTag("Thunder"))
        {
            Destroy(gameObject);

        }
    }
}
