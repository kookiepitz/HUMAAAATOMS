using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenFadeIn : MonoBehaviour
{
    public Image fadeImage;  // Reference to the white image
    public float fadeDuration = 3f;  // Duration for the fade to complete

    void Start()
    {
        // Start the fade-out process
        StartCoroutine(FadeOut());
    }

    // Coroutine to handle fade-out
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;  // Starting color (white)

        // Gradually reduce alpha to 0 (fade effect)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alphaValue);
            yield return null;
        }

        // Ensure the image is fully transparent after fade-out
        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }
}
