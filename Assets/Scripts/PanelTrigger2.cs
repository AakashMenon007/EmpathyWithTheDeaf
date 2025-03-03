using UnityEngine;

public class TriggerActivationWithDelay : MonoBehaviour
{
    [Tooltip("Drag the inactive prefab or GameObject you want to activate here")]
    public GameObject objectToActivate;

    [Tooltip("Time in seconds before the object is activated")]
    public float activateDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if entering object is the player
        if (other.CompareTag("Player"))
        {
            // Activate the prefab after the delay
            if (objectToActivate != null)
            {
                Invoke("ActivateObject", activateDelay);
            }
            else
            {
                Debug.LogWarning("Object to activate is not assigned!", this);
            }
        }
    }

    // Method to activate the object
    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned!", this);
        }
    }
}
