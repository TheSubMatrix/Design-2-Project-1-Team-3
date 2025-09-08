using UnityEngine;

public class Level2ToLvl2Dialog : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene("Level 2 Dialogue");
        }
    }
}
