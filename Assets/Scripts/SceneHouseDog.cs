using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHouseDog : MonoBehaviour
{
    [Header("GameObject References")]
    public GameObject dogObject;
    public GameObject NPC;
    public GameObject alarm;
    public GameObject canvasToEnable;
    public GameObject canvasToDisable;

    [Header("Player & Spawn")]
    public GameObject playerSpawnPoint;

    [Header("Scene Transition")]
    public string targetSceneName;

    [Header("Fade Settings")]
    public OVRScreenFade fade;

    // This static flag persists even if you reload the scene.
    private static bool firstTriggered = false;

    private void Start()
    {
        // On start, ensure these objects are in their proper initial state.
        if (dogObject != null)
            dogObject.SetActive(false); // Dog starts inactive
        if (NPC != null)
            NPC.SetActive(false);       // NPC starts inactive

        // Assume alarm and canvasToDisable are active by default.
        if (alarm != null)
            alarm.SetActive(true);
        if (canvasToDisable != null)
            canvasToDisable.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // First trigger: scene reset with modifications.
            if (!firstTriggered)
            {
                firstTriggered = true;
                // Pass the player's collider so we can reposition the player.
                StartCoroutine(FirstTriggerSequence(other));
            }
            // Second trigger: load target scene.
            else
            {
                StartCoroutine(SecondTriggerSequence());
            }
        }
    }

    private IEnumerator FirstTriggerSequence(Collider playerCollider)
    {
        // Fade out.
        if (fade != null)
            fade.FadeOut();
        yield return new WaitForSeconds(2.0f);

        // Change the player's spawn position.
        if (playerSpawnPoint != null)
        {
            playerCollider.transform.position = playerSpawnPoint.transform.position;
            Debug.Log("Player moved to spawn point: " + playerSpawnPoint.transform.position);
        }

        // Enable the dog, NPC, and the designated canvas.
        if (dogObject != null)
            dogObject.SetActive(true);
        if (NPC != null)
            NPC.SetActive(true);
        if (canvasToEnable != null)
            canvasToEnable.SetActive(true);

        // Disable the alarm and the canvas that should be hidden.
        if (alarm != null)
            alarm.SetActive(false);
        if (canvasToDisable != null)
            canvasToDisable.SetActive(false);

        // Fade in.
        if (fade != null)
            fade.FadeIn();
        yield return null;
    }

    private IEnumerator SecondTriggerSequence()
    {
        if (fade != null)
            fade.FadeOut();
        yield return new WaitForSeconds(2.0f);

        // Load the target scene.
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set.");
        }
        yield return null;
    }
}
