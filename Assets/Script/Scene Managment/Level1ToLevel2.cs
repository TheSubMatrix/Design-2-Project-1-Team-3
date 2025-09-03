using UnityEngine;

public class Level1ToLevel2 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene("Level 2");
        }
    }
}
