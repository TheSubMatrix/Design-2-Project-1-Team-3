using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene("Level 1");
    }
    public void OnQuitButtonPressed()
    {
        SceneTransitionManager.Instance.QuitApplication(0.5f);
    }
    public void OnCreditsButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene("Credits");
    }

    public void OnHelpButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene("Help");
    }
    
    public void OnBackButtonPressed()
    {
        SceneTransitionManager.Instance.TransitionToScene("Title");
    }
}
