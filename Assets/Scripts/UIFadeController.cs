using System.Collections;
using UnityEngine;

public class UIFadeController : MonoBehaviour
{
    [Header("Canvas Groups")]
    public CanvasGroup panel1;
    public CanvasGroup panel2;

    [Header("Audio Sources")]
    public AudioSource panel1Audio;
    public AudioSource panel2Audio;

    [Header("Fade Settings")]
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Initialize Panels
        InitializePanels();

        // Start with fading in Panel 1
        StartCoroutine(FadeInPanel1());
    }

    private void InitializePanels()
    {
        // Set Panel 1 hidden initially, will fade in
        panel1.alpha = 0;
        panel1.interactable = false;
        panel1.blocksRaycasts = false;

        // Set Panel 2 hidden
        panel2.alpha = 0;
        panel2.interactable = false;
        panel2.blocksRaycasts = false;

        Debug.Log("Panels Initialized. Panel 1 will fade in, Panel 2 is hidden.");
    }

    private IEnumerator FadeInPanel1()
    {
        Debug.Log("Fading in Panel 1...");

        // Fade in Panel 1
        yield return FadeCanvas(panel1, true);

        // Play Panel 1 audio after fade-in
        if (panel1Audio != null && !panel1Audio.isPlaying)
        {
            Debug.Log("Playing Panel 1 audio...");
            panel1Audio.Play();
        }

        // Wait for Panel 1 audio to finish, then transition
        StartCoroutine(HandleAudioAndTransition());
    }

    private IEnumerator HandleAudioAndTransition()
    {
        Debug.Log("Waiting for Panel 1 audio to finish...");

        // Wait until Panel 1 audio finishes playing
        while (panel1Audio.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Panel 1 audio finished. Fading out Panel 1...");
        yield return FadeCanvas(panel1, false);

        Debug.Log("Fading in Panel 2...");
        yield return FadeCanvas(panel2, true);

        // Play Panel 2 audio
        if (panel2Audio != null && !panel2Audio.isPlaying)
        {
            Debug.Log("Playing Panel 2 audio...");
            panel2Audio.Play();
        }
    }

    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, bool fadeIn)
    {
        float startAlpha = canvasGroup.alpha;
        float endAlpha = fadeIn ? 1.0f : 0.0f;
        float elapsedTime = 0.0f;

        Debug.Log($"Starting fade {(fadeIn ? "in" : "out")}...");

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            Debug.Log($"Canvas Alpha: {canvasGroup.alpha}");
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = fadeIn;
        canvasGroup.blocksRaycasts = fadeIn;

        Debug.Log($"Fade {(fadeIn ? "in" : "out")} complete. Final Alpha: {endAlpha}");
    }
}
