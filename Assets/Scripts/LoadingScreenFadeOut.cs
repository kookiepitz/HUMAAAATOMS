using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenFadeOut : MonoBehaviour
{
    public Slider progressBar;    // Reference to the slider
    public Image fillImage;       // Reference to the fill area of the slider
    public Camera mainCamera;     // Reference to the main camera
    public float fadeDuration = 2f; // Duration of the color fade effect

    void Start()
    {
        // Change the progress bar fill color to #B36E7B
        Color barColor;
        if (ColorUtility.TryParseHtmlString("#B36E7B", out barColor))
        {
            fillImage.color = barColor;
        }

        // Start loading the next scene asynchronously
        StartCoroutine(LoadSceneWithBackgroundFade("TutorialControlSample"));
    }

    IEnumerator LoadSceneWithBackgroundFade(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Prevent immediate scene activation

        while (operation.progress < 0.6f)
        {
            // Update the progress bar value gradually
            progressBar.value = Mathf.Lerp(progressBar.value, Mathf.Clamp01(operation.progress / 0.9f), Time.deltaTime * 5f);
            yield return null;
        }

        // Ensure the progress bar reaches full value
        while (progressBar.value < 1f)
        {
            progressBar.value = Mathf.Lerp(progressBar.value, 1f, Time.deltaTime * 5f);
            yield return null;
        }

        // Wait for an additional delay
        yield return new WaitForSeconds(1f);

        // Fade the camera's background to black
        yield return StartCoroutine(FadeBackgroundToWhite());

        // Allow scene activation
        operation.allowSceneActivation = true;
    }

    IEnumerator FadeBackgroundToWhite()
    {
        float elapsedTime = 0f;
        Color startColor = mainCamera.backgroundColor; // Get the current background color
        Color targetColor = Color.white;              // Target color is white

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration); // Smooth transition
            yield return null;
        }

        // Ensure the final color is set to black
        mainCamera.backgroundColor = targetColor;
    }
}
