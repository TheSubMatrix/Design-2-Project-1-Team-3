using UnityEngine;

public class OpeningDoor : MonoBehaviour
{
    public GameObject openDoor;
    public int key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            openDoor.SetActive(true);
            Destroy(gameObject);
            key += 1;
            if (key >= 1)
            {
                Destroy(openDoor);
            }
            


        }
    }
}
