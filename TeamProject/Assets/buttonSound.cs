using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound;   // The sound that will play when the button is clicked
    private AudioSource audioSource;     // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject (or add one if needed)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;  // Ensure the sound does not play automatically
    }

    // This method will be called when the button is clicked
    public void PlayButtonSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);  // Play the button click sound
        }
    }
}