using System.Collections;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] BasePickup m_pickupPrefab;
    [SerializeField] float m_spawnDelay = 5;
    void Start()
    {
        CreateNewPickup();
    }
    void CreateNewPickup()
    {
        GameObject newPickup = Instantiate(m_pickupPrefab.gameObject, transform.position, transform.rotation);
        newPickup.GetComponent<BasePickup>().OnPickup.AddListener(() => StartCoroutine(SpawnPickupAfterTimeAsync()));
    }
    IEnumerator SpawnPickupAfterTimeAsync()
    {
        yield return new WaitForSeconds(m_spawnDelay);
        CreateNewPickup();
    }
}
