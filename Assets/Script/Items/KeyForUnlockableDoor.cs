using UnityEngine;
using UnityEngine.Events;

public class KeyForUnlockableDoor : BasePickup
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Collect();
        }
    }
}
