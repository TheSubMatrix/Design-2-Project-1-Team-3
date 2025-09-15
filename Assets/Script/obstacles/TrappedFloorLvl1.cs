using UnityEngine;

public class TrappedFloorLvl1 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Destroy(gameObject);
            transform.Rotate(0, 0, 90);
        }
    }
}
