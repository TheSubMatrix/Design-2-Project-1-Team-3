using UnityEngine;

public class SceneTransitionCaller : MonoBehaviour
{
    public void TransitionToLevelOne()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 1");
    }
    public void TransitionToLevelTwo()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 2");
    }
    public void TransitionToLevelThree()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 3");
    }
    public void TransitionToLevelOneDialogue()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 1 Dialogue");
    }
    public void TransitionToLevelTwoDialogue()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Level 2 Dialogue");
    }
    public void TransitionToPostGameDialogue()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Post Game Dialogue");
    }
    public void TransitionToCredits()
    {
        SceneTransitionManager.Instance?.TransitionToScene("Credits");
    }
}
