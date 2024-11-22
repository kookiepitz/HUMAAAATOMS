using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public PanelSwitcher panelSwitcher; // Reference to the PanelSwitcher script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger zone
        if (other.CompareTag("Player"))
        {
            // Switch to the next panel
            panelSwitcher.SwitchToNextPanel();
        }
    }
}
