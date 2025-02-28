using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHouseDog : MonoBehaviour
{
    public GameObject dogObject; // Assign your Dog GameObject in the Inspector
    public GameObject spawnPoint;
    public GameObject NPC;

    public OVRScreenFade fade;

    private bool flag = false;          //assigns the flag to false, for scene change condition

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
        if (other.CompareTag("Player") && flag == false) // condition tag player and flag check
        {
            StartCoroutine(MovePlayer());
        }
        else
        {
            //Scene change to AIRTABLE scene
            Debug.Log("Player has already entered the trigger");
        }
    }
    public IEnumerator MovePlayer()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(2.0f);

        transform.position = spawnPoint.transform.position;     // Move the player to the spawn point after scene change
        Debug.Log($"Player moved to position: {spawnPoint.transform.position}");

        if (dogObject != null && NPC != null)
        {
            dogObject.SetActive(true); // Activate the dog when the player enters the trigger
            NPC.SetActive(true);
        }

        fade.FadeIn();
        flag = true; // Set the flag to true to prevent the scene change from happening again
    }
}
