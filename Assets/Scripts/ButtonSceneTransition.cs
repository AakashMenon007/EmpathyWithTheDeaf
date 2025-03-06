using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneTransition2 : MonoBehaviour
{
    // This method will be linked to the OnClick event of the button
    public void LoadNextScene()
    {
        // Get the current active scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene based on the build index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
