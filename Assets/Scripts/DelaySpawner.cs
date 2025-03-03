using UnityEngine;

public class DelayedActivation : MonoBehaviour
{
    public GameObject targetObject; // The object to activate after delay
    public BoxCollider triggerCollider; // The collider to detect the contact
    public float delayTime = 2f; // Delay time in seconds before activation

    private void OnTriggerEnter(Collider other)
    {
        if (other == triggerCollider) // Check if the object enters the specified collider
        {
            Invoke("ActivateObject", delayTime); // Call the ActivateObject method after the delay
        }
    }

    private void ActivateObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Activate the target object
        }
    }
}
