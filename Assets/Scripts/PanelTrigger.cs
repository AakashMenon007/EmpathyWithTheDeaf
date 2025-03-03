using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    [Tooltip("Drag the inactive prefab or GameObject you want to activate here")]
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        // Check if entering object is the player
        if (other.CompareTag("Player"))
        {
            // Activate the prefab if it's assigned
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

    // New method for button click deactivation
    public void DeactivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object to deactivate is not assigned!", this);
        }
    }
}