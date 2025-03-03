using UnityEngine;

public class ButtonPrefabToggle : MonoBehaviour
{
    public GameObject prefabToTurnOff; // The prefab to deactivate when the button is pressed

    // This method can be called by the Button onClick event
    public void TurnOffPrefab()
    {
        if (prefabToTurnOff != null)
        {
            prefabToTurnOff.SetActive(false);  // Deactivate the prefab
        }
        else
        {
            Debug.LogError("Prefab to turn off is not assigned!");
        }
    }
}
