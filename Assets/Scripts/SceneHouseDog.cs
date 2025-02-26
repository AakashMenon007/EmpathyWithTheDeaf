using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHouseDog : MonoBehaviour
{
    public GameObject dogObject; // Assign your Dog GameObject in the Inspector
    public GameObject spawnPoint;

    public OVRScreenFade fade;

    private void Start()
    {
        if (dogObject != null)
        {
            //
            dogObject.SetActive(false); // Ensure dog starts as inactive
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            StartCoroutine(MovePlayer());
        }
    }
    public IEnumerator MovePlayer()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(2.0f);

        transform.position = spawnPoint.transform.position;     // Move the player to the spawn point after scene change
        Debug.Log($"Player moved to position: {spawnPoint.transform.position}");

        if (dogObject != null)
        {
            dogObject.SetActive(true); // Activate the dog when the player enters the trigger
        }

        fade.FadeIn();
    }
}
