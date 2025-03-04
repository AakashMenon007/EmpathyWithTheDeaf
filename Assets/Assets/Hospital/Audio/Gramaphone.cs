using UnityEngine;

public class PokeInteraction : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator
    public AudioSource audioSource;  // Reference to the AudioSource
    public string animationTrigger = "PokeAnimation";  // Name of the animation trigger

    private bool isActive = false;  // Tracks whether the interaction is active

    void Update()
    {
        // Check if the "Poke" button is pressed
        if (Input.GetButtonDown("Poke"))
        {
            ToggleAnimationAndAudio(); // Toggle the animation and audio
        }
    }

    // Function to toggle both animation and audio
    public void ToggleAnimationAndAudio()
    {
        if (isActive)
        {
            // Turn off animation and audio
            animator.SetBool(animationTrigger, false);  // Set the animation to false (stop)
            audioSource.Stop();  // Stop the audio
        }
        else
        {
            // Turn on animation and audio
            animator.SetBool(animationTrigger, true);  // Set the animation to true (play)
            audioSource.Play();  // Play the audio
        }

        // Toggle the state
        isActive = !isActive;
    }
}
