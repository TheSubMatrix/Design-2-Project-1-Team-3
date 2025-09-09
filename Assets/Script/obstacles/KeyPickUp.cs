using UnityEngine;

public class DoorAndKey : MonoBehaviour
{
    public int key;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        key += 1;
        Debug.Log("key");
        Destroy(gameObject);
        
    }
}
