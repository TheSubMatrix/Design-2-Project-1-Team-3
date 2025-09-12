using System.Collections;
using UnityEngine;

public class FadeCanvasGroup : MonoBehaviour
{
    [SerializeField]CanvasGroup m_canvasGroup;
    public void FadeOutCanvasGroup()
    {
        StartCoroutine(FadeCanvasGroupAsync(m_canvasGroup, 0, 0.5f));
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
