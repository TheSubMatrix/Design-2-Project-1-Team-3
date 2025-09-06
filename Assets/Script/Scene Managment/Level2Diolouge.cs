using UnityEngine;

public class Level2Diolouge : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene("Level2Dialouge");
        }
    }
}
