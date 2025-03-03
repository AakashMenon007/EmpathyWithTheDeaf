using UnityEngine;

public class GreetAnimationTrigger : MonoBehaviour
{
    [Tooltip("Assign the Animator of the character here")]
    public Animator characterAnimator;

    [Tooltip("Set the trigger name for the greet animation")]
    public string greetTrigger = "Greet";

    [Tooltip("Set the trigger name for the idle animation")]
    public string idleTrigger = "Idle";

    [Tooltip("Assign the AudioSource component here")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Check if entering object is the player
        if (other.CompareTag("Player"))
        {
            // Trigger the greet animation
            if (characterAnimator != null)
            {
                characterAnimator.SetTrigger(greetTrigger);
            }
            else
            {
                Debug.LogWarning("Animator not assigned!", this);
            }

            // Play the audio if it's assigned
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource not assigned or already playing!", this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player"))
        {
            // Return to idle animation
            if (characterAnimator != null)
            {
                characterAnimator.SetTrigger(idleTrigger);
            }
            else
            {
                Debug.LogWarning("Animator not assigned!", this);
            }

            // Stop the audio when the player exits
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                Debug.LogWarning("AudioSource not assigned or not playing!", this);
            }
        }
    }
}
