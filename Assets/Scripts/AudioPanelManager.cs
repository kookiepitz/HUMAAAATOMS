using UnityEngine;

public class AudioPanelManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public GameObject[] panels;     // Array of panels
    public AudioClip[] audioClips;  // Array of audio clips corresponding to each panel
    private int currentPanelIndex = 0;

    void Start()
    {
        // Ensure only the first panel is active initially
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        // Automatically play audio for the first panel
        PlayAudioForCurrentPanel();
    }

    private void PlayAudioForCurrentPanel()
    {
        if (audioSource != null && currentPanelIndex < audioClips.Length)
        {
            // Assign the correct audio clip to the audio source
            audioSource.clip = audioClips[currentPanelIndex];

            if (audioSource.clip != null)
            {
                audioSource.Play();
                // Wait for the audio to finish, then show the next panel
                StartCoroutine(WaitForAudioToFinish());
            }
            else
            {
                Debug.LogError($"AudioClip for panel {currentPanelIndex} is not assigned!");
            }
        }
        else
        {
            Debug.LogError("AudioSource is not assigned or audio clips are missing!");
        }
    }

    private System.Collections.IEnumerator WaitForAudioToFinish()
    {
        // Wait until the audio finishes playing
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Automatically show the next panel
        ShowNextPanel();
    }

    public void ShowNextPanel()
    {
        // Hide the current panel
        panels[currentPanelIndex].SetActive(false);

        // Move to the next panel
        currentPanelIndex++;

        if (currentPanelIndex < panels.Length)
        {
            // Show the next panel
            panels[currentPanelIndex].SetActive(true);

            // Play the audio for the next panel
            PlayAudioForCurrentPanel();
        }
        else
        {
            Debug.Log("No more panels!");
        }
    }
}
