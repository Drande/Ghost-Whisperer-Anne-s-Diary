using System.Collections;
using UnityEngine;

public static class Coroutines {
    
    public static IEnumerator FadeIn(GameObject target, float fadeDuration = 0.5f)
    {
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            target.SetActive(true);
            float startAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 1f; // Ensure the final alpha is set to 1
        }
        else
        {
            Debug.LogWarning("GameObject does not have a CanvasGroup component.");
        }
    }

    public static IEnumerator FadeOut(GameObject target, float fadeDuration = 0.5f)
    {
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float startAlpha = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 0f; // Ensure the final alpha is set to 0
            target.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameObject does not have a CanvasGroup component.");
        }
    }
}