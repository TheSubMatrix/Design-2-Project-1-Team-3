using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomItemDrops : MonoBehaviour
{
    [SerializeField]List<GameObject> m_itemDropPrefabs = new();
    
    public void OnEnemyDeath()
    {
        if(m_itemDropPrefabs is null || m_itemDropPrefabs.Count <= 0) return;
        bool shouldDrop = Random.Range(0, 100) < 50;
        if (!shouldDrop) return;
        int powerUpSelected = Random.Range(0, m_itemDropPrefabs.Count);
        Instantiate(m_itemDropPrefabs[powerUpSelected], transform.position, Quaternion.identity);
    }
}
