using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Tooltip("Assign the AudioSource component here")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            // Play the audio if it's assigned and not already playing
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else if (audioSource == null)
            {
                Debug.LogWarning("AudioSource not assigned!", this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            // Stop the audio if it's assigned and currently playing
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else if (audioSource == null)
            {
                Debug.LogWarning("AudioSource not assigned!", this);
            }
        }
    }
}
