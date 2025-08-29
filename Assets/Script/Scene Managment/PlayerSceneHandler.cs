using UnityEngine;

public class PlayerSceneHandler : MonoBehaviour
{
    public void OnPlayerDeath()
    {
        SceneTransitionManager.Instance.ReloadScene(0.5f);
    }
}
