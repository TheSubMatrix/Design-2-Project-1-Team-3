using UnityEngine;

public class Teleport : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            transform.position = new Vector3(475.7f, 105f, -71.2f);
        }
    }
}
