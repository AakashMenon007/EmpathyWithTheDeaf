using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace

public class PokeInteraction : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator
    public AudioSource audioSource;  // Reference to the AudioSource
    public string animationTrigger = "PokeAnimation";  // Name of the animation trigger

    private bool isActive = false;  // Tracks whether the interaction is active
    private InputAction pokeAction; // InputAction for the "Poke" button

    private void OnEnable()
    {
        // Initialize the InputAction
        pokeAction = new InputAction("Poke", binding: "<Keyboard>/p"); // Bind to the "P" key
        pokeAction.performed += ctx => ToggleAnimationAndAudio(); // Subscribe to the performed event
        pokeAction.Enable(); // Enable the InputAction
    }

    private void OnDisable()
    {
        // Disable and dispose of the InputAction
        pokeAction.Disable();
        pokeAction.Dispose();
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
