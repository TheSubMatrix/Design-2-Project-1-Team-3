using UnityEngine;

public class TitleSceneUIManager : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene("Level 1");
    }
}
