using UnityEngine;

public class ScrollPopUpPanel : MonoBehaviour
{
    public GameObject scrollPanel;    // The scroll panel to animate
    public float animationDuration = 0.5f; // Duration of the pop-up animation

    private Vector3 initialScale;     // Initial scale of the panel
    private Vector3 targetScale;      // Target scale of the panel
    private bool isAnimating = false; // Flag to check if animation is in progress
    private float elapsedTime = 0f;   // Timer for animation progress

    void Start()
    {
        // Store the current scale from the Inspector as the target scale
        targetScale = scrollPanel.transform.localScale;

        // Set the initial scale by collapsing only the Y axis
        initialScale = new Vector3(targetScale.x, 0, targetScale.z);

        // Set the panel's scale to the initial collapsed state
        scrollPanel.transform.localScale = initialScale;

        // Start the animation immediately (optional)
        StartAnimation();
    }

    void Update()
    {
        if (isAnimating)
        {
            // Calculate animation progress
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / animationDuration);

            // Smoothly interpolate the Y scale from initial to target
            float newYScale = Mathf.Lerp(initialScale.y, targetScale.y, progress);
            scrollPanel.transform.localScale = new Vector3(targetScale.x, newYScale, targetScale.z);

            // End animation once complete
            if (progress >= 1f)
            {
                isAnimating = false;
            }
        }
    }

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            elapsedTime = 0f; // Reset the timer for the animation
        }
    }
}
