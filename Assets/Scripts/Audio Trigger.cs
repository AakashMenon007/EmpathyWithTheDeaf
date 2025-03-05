using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Tooltip("Assign the AudioSource components here")]
    public AudioSource[] audioSources;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            foreach (var audioSource in audioSources)
            {
                // Play the audio if it's assigned and not already playing
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                else if (audioSource == null)
                {
                    Debug.LogWarning("An AudioSource is not assigned!", this);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            foreach (var audioSource in audioSources)
            {
                // Stop the audio if it's assigned and currently playing
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                else if (audioSource == null)
                {
                    Debug.LogWarning("An AudioSource is not assigned!", this);
                }
            }
        }
    }
}
