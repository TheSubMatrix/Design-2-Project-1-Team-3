using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public DoorAndKey keyPickUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyPickUp.key += 1;
    }

    // Update is called once per frame
    void Update()
    {
        int value = keyPickUp.key;
        //Debug.Log("Value from key: " + value);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && keyPickUp.key >= 1)
        {
            Destroy(gameObject);
            Debug.Log("player has: " + keyPickUp.key + " key ");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player") && keyPickUp.key < 1)
        {
            Debug.Log("no  key");
            Debug.Log("player has: " + keyPickUp.key + " key ");
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("player has a layer");
            Debug.Log("player has: " + keyPickUp.key + " key ");
        }
        else 
        {
            Debug.Log("error");
        }
    }
}
