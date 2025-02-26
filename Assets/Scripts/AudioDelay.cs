using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    [Tooltip("The AudioSource to play after the delay.")]
    public AudioSource audioSource;

    [Tooltip("The delay in seconds before the audio starts playing.")]
    public float delayInSeconds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null)
        {
            StartCoroutine(PlayAudioWithDelay());
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned. Please assign an AudioSource in the Inspector.");
        }
    }

    private System.Collections.IEnumerator PlayAudioWithDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayInSeconds);

        // Play the audio
        audioSource.Play();
    }
}
