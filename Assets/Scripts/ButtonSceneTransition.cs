using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // Function to be called on button press
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
