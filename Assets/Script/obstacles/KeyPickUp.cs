using UnityEngine;

public class DoorAndKey : MonoBehaviour
{
    public int key = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        Destroy(gameObject);
        key += 1;
    }
}
