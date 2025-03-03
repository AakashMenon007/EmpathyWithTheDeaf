using UnityEngine;

public class PrefabSwitcher : MonoBehaviour
{
    public GameObject currentPrefab;  // The prefab to deactivate
    public GameObject nextPrefab;     // The prefab to activate

    // This method can be called by the Button onClick event
    public void SwitchPrefab()
    {
        if (currentPrefab != null)
        {
            currentPrefab.SetActive(false);  // Deactivate the current prefab
        }

        if (nextPrefab != null)
        {
            nextPrefab.SetActive(true);  // Activate the next prefab
        }
    }
}
