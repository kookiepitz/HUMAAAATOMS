using UnityEngine;
using UnityEngine.UI;

public class PopupCanvasController : MonoBehaviour
{
    public Canvas popupCanvas; // Drag the canvas here
    public float delay = 3f; // Delay before showing the canvas
    public Transform player; // Reference to the player

    private void Start()
    {
        // Initially, hide the canvas
        popupCanvas.gameObject.SetActive(false);
        // Start the delay effect
        Invoke("ShowPopup", delay);
    }

    private void Update()
    {
        // Make the canvas face the player
        Vector3 directionToPlayer = player.position - popupCanvas.transform.position;
        directionToPlayer.y = 0; // Keep the canvas upright (no vertical tilt)
        popupCanvas.transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    private void ShowPopup()
    {
        // Show the popup after the delay
        popupCanvas.gameObject.SetActive(true);
    }
}
