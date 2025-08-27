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

    public void TransitionToScene(string sceneName, float transitionTime = 0.5f)
    {
        StartCoroutine(OnSceneTransition(sceneName, transitionTime));
    }
    
    public void ReloadScene(float transitionTime)
    {
        StartCoroutine(OnSceneTransition(SceneManager.GetActiveScene().name, transitionTime));
    }
    public void QuitApplication(float transitionTime)
    {
        StartCoroutine(QuitApplicationAsync(transitionTime));
    }
    IEnumerator OnSceneTransition(string sceneName, float transitionTime)
    {
        if (IsTransitioning) yield break;
        yield return FadeCanvasGroupAsync(m_transitionCanvasGroup, 1, transitionTime);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return FadeCanvasGroupAsync(m_transitionCanvasGroup, 0, transitionTime);
    }

    IEnumerator QuitApplicationAsync(float transitionTime)
    {
        yield return FadeCanvasGroupAsync(m_transitionCanvasGroup, 1, transitionTime);
        Application.Quit();
    }
    static IEnumerator FadeCanvasGroupAsync(CanvasGroup canvasGroup, float targetAlpha, float transitionTime)
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
