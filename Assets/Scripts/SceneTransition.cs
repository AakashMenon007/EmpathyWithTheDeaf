using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneTransition : MonoBehaviour
{
    [Tooltip("Time in seconds before the scene transition occurs.")]
    public float delayTime = 5f; // Delay before the transition

    [Tooltip("Name of the scene to load.")]
    public string targetSceneName; // The name of the scene to load

    void Start()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // Start the delayed scene transition
            StartCoroutine(TransitionAfterDelay());
        }
        else
        {
            Debug.LogWarning("Target scene name is not set. Please assign a valid scene name in the Inspector.");
        }
    }

    private IEnumerator TransitionAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }
}
