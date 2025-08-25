using System.Collections;
using CustomNamespace.GenericDatatypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    [SerializeField] CanvasGroup m_transitionCanvasGroup;
    public bool IsTransitioning => m_transitionCanvasGroup.alpha > 0;
    protected override void InitializeSingleton()
    {
        base.InitializeSingleton();
        DontDestroyOnLoad(gameObject);
        m_transitionCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void TransitionToScene(string sceneName, float transitionTime)
    {
        StartCoroutine(OnSceneTransition(sceneName, transitionTime));
    }
    
    public void ReloadScene(float transitionTime)
    {
        StartCoroutine(OnSceneTransition(SceneManager.GetActiveScene().name, transitionTime));
    }
    
    IEnumerator OnSceneTransition(string sceneName, float transitionTime)
    {
        if (IsTransitioning) yield break;
        yield return FadeCanvasGroup(m_transitionCanvasGroup, 1, transitionTime);
        SceneManager.LoadScene(sceneName);
        yield return FadeCanvasGroup(m_transitionCanvasGroup, 0, transitionTime);
    }

    static IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float transitionTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, elapsedTime / transitionTime);
            canvasGroup.blocksRaycasts = canvasGroup.alpha > 0;
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0;
    }
    
}
