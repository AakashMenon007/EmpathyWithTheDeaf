using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHouseDog : MonoBehaviour
{
    public GameObject dogObject; // Assign your Dog GameObject in the Inspector
    public GameObject spawnPoint;

    public GameObject alarm;

    public GameObject NPC;

    public OVRScreenFade fade;

    private bool flag = true;          //assigns the flag to false, for scene change condition in the house scene.

    [Tooltip("Name of the scene to load.")]
    public string targetSceneName; // The name of the scene to load

    private void Start()
    {
        if (dogObject != null && NPC != null)
        {
            //
            dogObject.SetActive(false); // Ensure dog starts as inactive
            NPC.SetActive(false);   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && flag == true) // condition tag player and flag check
        {
            StartCoroutine(MovePlayer());
        }
        else
        {
            //Scene change to AIRTABLE scene
            Debug.Log("Player has already entered the trigger");

            if (!string.IsNullOrEmpty(targetSceneName))
            {
                // Start the delayed scene transition
                StartCoroutine(TransitionAfterDelay());
            }
            else
            {
                Debug.LogWarning("Target scene name is not set");
            }
        }
    }

    private IEnumerator TransitionAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(2.0f);

        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }

    public IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(2.0f);
        fade.FadeOut();

        transform.position = spawnPoint.transform.position;     // Move the player to the spawn point after scene change
        Debug.Log($"Player moved to position: {spawnPoint.transform.position}");

        //if (dogObject != null && NPC != null)
        //{
        //    dogObject.SetActive(true); // Activate the dog when the player enters the trigger
        //    NPC.SetActive(true);
        //}

        dogObject.SetActive(true); // Activate the dog when the player enters the trigger
        NPC.SetActive(true);
        alarm.SetActive(false);

        fade.FadeIn();
        flag = false; // Set the flag to true to prevent the scene change from happening again
    }
}
