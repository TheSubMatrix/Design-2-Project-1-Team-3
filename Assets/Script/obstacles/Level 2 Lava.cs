using UnityEngine;

public class Level2Lava : MonoBehaviour
{
    public GameObject LavaHot;
    public GameObject LavaCool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LavaHot.SetActive(true);
        LavaCool.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice"))
        {
            LavaHot.SetActive(false);
            LavaCool.SetActive(true);

        }
    }
}
