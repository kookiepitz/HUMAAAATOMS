using System.Collections;
using UnityEngine;

public class UIFadeManager : MonoBehaviour
{
    [Header("Panel Settings")]
    public CanvasGroup panel1; // First panel's CanvasGroup
    public CanvasGroup panel2; // Second panel's CanvasGroup

    [Header("Audio Sources")]
    public AudioSource panel1Audio; // Audio for panel 1
    public AudioSource panel2Audio; // Audio for panel 2

    [Header("Fade Settings")]
    public float fadeDuration = 1.0f; // Duration of fade in/out in seconds

    private void Start()
    {
        // Ensure initial panel visibility
        panel1.alpha = 1;
        panel1.interactable = true;
        panel1.blocksRaycasts = true;

        panel2.alpha = 0;
        panel2.interactable = false;
        panel2.blocksRaycasts = false;

        // Start handling the transitions
        StartCoroutine(HandlePanelTransition());
    }

    private IEnumerator HandlePanelTransition()
    {
        // Wait for Panel 1's audio to finish
        if (panel1Audio != null)
        {
            panel1Audio.Play();
            while (panel1Audio.isPlaying)
            {
                yield return null;
            }
        }

        // Fade out Panel 1
        yield return FadeCanvas(panel1, false);

        // Fade in Panel 2
        yield return FadeCanvas(panel2, true);

        // Play Panel 2's audio if available
        if (panel2Audio != null)
        {
            panel2Audio.Play();
        }
    }

    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, bool fadeIn)
    {
        float startAlpha = canvasGroup.alpha;
        float endAlpha = fadeIn ? 1.0f : 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // Enable or disable interactions
        canvasGroup.interactable = fadeIn;
        canvasGroup.blocksRaycasts = fadeIn;
    }
}
