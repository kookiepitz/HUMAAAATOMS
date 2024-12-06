using UnityEngine;

public class VRChecklist : MonoBehaviour
{
    private bool movedForward = false;
    private bool movedBackward = false;
    private bool movedSideways = false;

    public GameObject progressionPanel; // UI feedback for progression (optional)

    private Vector3 initialPosition;

    void Start()
    {
        // Store the player's starting position
        initialPosition = transform.position;

        // Hide the progression panel at the start (if assigned)
        if (progressionPanel != null)
            progressionPanel.SetActive(false);
    }

    void Update()
    {
        // Check for forward movement
        if (!movedForward && transform.position.z > initialPosition.z + 0.5f)
        {
            movedForward = true;
            Debug.Log("Moved Forward!");
        }

        // Check for backward movement
        if (!movedBackward && transform.position.z < initialPosition.z - 0.5f)
        {
            movedBackward = true;
            Debug.Log("Moved Backward!");
        }

        // Check for sideways movement
        if (!movedSideways && Mathf.Abs(transform.position.x - initialPosition.x) > 0.5f)
        {
            movedSideways = true;
            Debug.Log("Moved Sideways!");
        }

        // Check if all actions are completed
        if (movedForward && movedBackward && movedSideways)
        {
            OnChecklistComplete();
        }
    }

    private void OnChecklistComplete()
    {
        Debug.Log("All tasks completed!");

        // Show progression panel if assigned
        if (progressionPanel != null)
            progressionPanel.SetActive(true);

        // Add progression logic (e.g., unlock door, load next level, etc.)
        // Example:
        // Door.Unlock();
    }
}
