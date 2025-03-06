using System.Collections; // Required for IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    // Delay before the scene transition (in seconds)
    public float delay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Start the scene transition with a delay
            StartCoroutine(TransitionAfterDelay2());
        }
    }

    private IEnumerator TransitionAfterDelay2()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
