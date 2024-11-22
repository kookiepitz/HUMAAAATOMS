using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject[] panels; // Array of panels to switch between
    private int currentPanelIndex = 0;

    void Start()
    {
        // Ensure only the first panel is active at the start
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }
    }

    public void SwitchToNextPanel()
    {
        // Deactivate the current panel
        panels[currentPanelIndex].SetActive(false);

        // Move to the next panel (loop back to the first if at the end)
        currentPanelIndex = (currentPanelIndex + 1) % panels.Length;

        // Activate the next panel
        panels[currentPanelIndex].SetActive(true);
    }
}
