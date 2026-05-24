using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource source; // Drag your AudioSource here in the Inspector
    public AudioClip clickClip; // Optional: Drag a specific sound clip here

    // Method to be called by the Button's OnClick event
    public void PlayButtonSound()
    {
        // Option 1: Play the clip currently assigned to the AudioSource
        source.Play();

        // Option 2: Play a specific clip once (better if using one source for many buttons)
        // source.PlayOneShot(clickClip);
    }
}
