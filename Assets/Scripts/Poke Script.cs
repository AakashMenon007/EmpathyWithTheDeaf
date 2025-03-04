using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPokeToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToToggle; // Array of prefabs to toggle
    private bool isToggledOff = false; // Track the toggle state

    public void TogglePrefabs()
    {
        // Toggle the active state of all prefabs
        isToggledOff = !isToggledOff;

        foreach (GameObject prefab in prefabsToToggle)
        {
            if (prefab != null)
            {
                prefab.SetActive(!isToggledOff);
            }
            else
            {
                Debug.LogWarning("A prefab in the list is null!");
            }
        }
    }
}

