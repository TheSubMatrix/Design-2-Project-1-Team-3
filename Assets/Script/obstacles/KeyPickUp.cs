using UnityEngine;

public class DoorAndKey : MonoBehaviour
{
    public int key = 0;
    private void Start()
    {
    }
    public void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        key += 1;
        Debug.Log("player has: " + key + " key ");
        Destroy(gameObject);
        
    }
}
