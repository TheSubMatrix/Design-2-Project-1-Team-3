using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public DoorAndKey keyPickUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && keyPickUp.key >= 1)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player") && keyPickUp.key < 1)
        {
            Debug.Log("no  key");
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player has a layer");
        }
        else 
        {
            Debug.Log("error");
        }
    }
}
