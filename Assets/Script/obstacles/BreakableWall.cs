using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);

        }
    }
}
