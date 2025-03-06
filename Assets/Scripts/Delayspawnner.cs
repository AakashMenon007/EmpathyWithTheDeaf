using System.Collections;
using UnityEngine;

public class TurnOnPrefab : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab you want to turn on
    public float delayTime = 2f; // Time delay in seconds before the prefab turns on

    void Start()
    {
        // Disable the prefab initially
        prefab.SetActive(false);

        // Start the coroutine to turn on the prefab after the delay
        StartCoroutine(TurnOnAfterDelay());
    }

    IEnumerator TurnOnAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayTime);

        // Turn on the prefab (enable it)
        prefab.SetActive(true);
    }
}
