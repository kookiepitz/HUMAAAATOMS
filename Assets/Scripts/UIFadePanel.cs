using System.Collections;
using UnityEngine;

public class UIFadePanel : MonoBehaviour
{
    [Header("Panel Settings")]
    public CanvasGroup canvasGroup; // The CanvasGroup of this panel
    public AudioSource audioSource; // The AudioSource attached to this panel
    public UIFadePanel nextPanel;   // Reference to the next panel to transition to

    [Header("Fade Settings")]
    public float fadeDuration = 1.0f; // Duration of fade in/out in seconds

    private void Start()
    {
        // Ensure this panel is fully visible if it’s the first panel
        if (nextPanel == null)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            // Hide all other panels initially
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        // If there's an audio source, play it
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Start the coroutine to check audio and transition
        StartCoroutine(HandleAudioAndTransition());
    }

    private IEnumerator HandleAudioAndTransition()
    {
        // Wait until the audio finishes playing
        if (audioSource != null)
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }

        // Fade out this panel
        yield return FadeCanvas(false);

        // Trigger the next panel if it exists
        if (nextPanel != null)
        {
            yield return nextPanel.FadeCanvas(true);
        }
    }

    public IEnumerator FadeCanvas(bool fadeIn)
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
