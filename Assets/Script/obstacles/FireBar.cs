using UnityEngine;

public class FireBar : MonoBehaviour
{
    private float fanSpeed = 75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, fanSpeed * Time.deltaTime, 0);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player takes damage
        }
    }
}
