using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.transform.position  = new Vector3(-475.7f, 105f, -71.2f);
        }
    }
}
