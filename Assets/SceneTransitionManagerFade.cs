using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManagerFade : MonoBehaviour
{

    public fadescreen fadescreen;
    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadescreen.FadeOut();
        yield return new WaitForSeconds(fadescreen.fadeDuration);

        //launch New Scene
        SceneManager.LoadScene(sceneIndex);
    }
}
