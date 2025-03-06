using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHouseDog : MonoBehaviour
{
    [Header("GameObject References")]
    public GameObject dogObject;
    public GameObject NPC;
    public GameObject alarm;
    public GameObject kettle;
    private Vector3 kettleResetPos;
    private quaternion kettleResetRot;
    public GameObject canvasToEnable;
    public GameObject canvasToDisable;


    [Header("Player & Spawn")]
    public GameObject player;
    public GameObject playerSpawnPoint;

    [Header("Scene Transition")]
    public string targetSceneName;

    [Header("Fade Settings")]
    public OVRScreenFade fade;

    public UnityEngine.XR.Content.Interaction.XRKnob knob;      //knob reference for to reset after trigger

    // This static flag persists even if you reload the scene.
    public static bool firstTriggered = false;

    private void Start()
    {
        // On start, ensure these objects are in their proper initial state.
        //if (dogObject != null)
        //    dogObject.SetActive(false); // Dog starts inactive

        if (NPC != null)
            NPC.SetActive(false);       // NPC starts inactive

        // Assume alarm and canvasToDisable are active by default.
        //if (alarm != null)
        //    alarm.SetActive(true);

        kettleResetPos = kettle.transform.position;
        kettleResetRot = kettle.transform.rotation;

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
                StartCoroutine(FirstTriggerSequence());
            }
            // Second trigger: load target scene.
            else
            {
                StartCoroutine(SecondTriggerSequence());
            }
        }
    }

    private IEnumerator FirstTriggerSequence()
    {
        // Fade out.
        fade.FadeOut();
        yield return new WaitForSeconds(3f);

        // Change the player's spawn position.
        if (playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.transform.position;
            Debug.Log("Player moved to spawn point: " + playerSpawnPoint.transform.position);
        }

        // Enable the dog, NPC, and the designated canvas.
        if (dogObject != null)
        {
            dogObject.SetActive(true);

            // Re-enable the dog's NavMeshAgent after reactivation
            DogBehavior dogScript = dogObject.GetComponent<DogBehavior>();
            if (dogScript != null && dogScript.navMeshAgent != null)
            {
                dogScript.navMeshAgent.isStopped = false;
                dogScript.navMeshAgent.enabled = true;
            }
        }

        if (NPC != null) NPC.SetActive(true);
        if (canvasToEnable != null) canvasToEnable.SetActive(true);

        // Disable the alarm and the canvas that should be hidden.
        if (alarm != null) alarm.SetActive(false);
        if (canvasToDisable != null) canvasToDisable.SetActive(false);

        // Reset Kettle and Knob
        kettle.transform.SetPositionAndRotation(kettleResetPos, kettleResetRot);
        knob.value = 0; // Proper knob reset

        Debug.Log("Kettle and knob reset==============================");

        // Fade in after ensuring everything is active.
        fade.FadeIn();
    }


    private IEnumerator SecondTriggerSequence()
    {

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
