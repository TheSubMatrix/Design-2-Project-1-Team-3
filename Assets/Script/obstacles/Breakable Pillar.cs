using UnityEngine;

public class BreakablePillar : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(gameObject);

        }
        else if (other.gameObject.CompareTag("Ice") || other.gameObject.CompareTag("Thunder"))
        {
            //plays an auido of incorrect sound
        }
        if (other.gameObject.CompareTag("Player"))
        {
            //takes damage if gets moves backwards (so the player doesnt get comboed)
        }
    }
}
