using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHouseDog : MonoBehaviour
{
    public GameObject dogObject; // Assign your Dog GameObject in the Inspector

    private void Start()
    {
        if (dogObject != null)
        {
            dogObject.SetActive(false); // Ensure dog starts as inactive
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            if (dogObject != null)
            {
                dogObject.SetActive(true); // Activate the dog when the player enters the trigger
            }
        }
    }
}
