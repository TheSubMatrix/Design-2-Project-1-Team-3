using UnityEngine;

public class SceneTransitionCaller : MonoBehaviour
{
    public void TransitionToLevelTwo()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 2");
    }
}
