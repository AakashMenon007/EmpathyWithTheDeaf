using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitioner111 : MonoBehaviour
{
    // Name of the scene to load
    [SerializeField] private string sceneToLoad;

    // Delay in seconds before transitioning
    [SerializeField] private float delay = 2f;

    // Reference to the box collider
    [SerializeField] private Collider targetCollider;

    // Check if the transition has already been triggered
    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the specified collider has been hit and the player triggered it
        if (other == targetCollider && other.CompareTag("Player") && !isTransitioning)
        {
            Debug.Log($"Player entered the collider. Transition to '{sceneToLoad}' will begin in {delay} seconds.");
            isTransitioning = true; // Prevent multiple triggers
            StartCoroutine(TransitionScene());
        }
    }

    private IEnumerator TransitionScene()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        Debug.Log($"Transitioning to scene: {sceneToLoad}");
        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
