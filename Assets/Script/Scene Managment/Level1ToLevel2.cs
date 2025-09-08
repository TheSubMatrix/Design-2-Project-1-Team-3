using UnityEngine;

public class Level1ToLevel2 : MonoBehaviour
{
    //changed to go to the end of level 1 text
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene("Level 1 Dialogue");
        }
    }
}
