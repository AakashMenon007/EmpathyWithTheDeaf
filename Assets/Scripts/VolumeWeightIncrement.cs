using UnityEngine;
using UnityEngine.Rendering;

public class VolumeWeightLerp : MonoBehaviour
{
    [Header("Volume Settings")]
    public Volume volume;       // The volume whose weight you want to adjust
    public float duration = 5f; // Duration over which the weight will go from 0 to 1

    private float elapsedTime = 0f;

    void Start()
    {
        if (volume != null)
        {
            volume.weight = 0f;
            Debug.Log("Volume weight set to 0 at start. Beginning weight increase.");
        }
        else
        {
            Debug.LogError("Volume not assigned in Inspector.");
        }
    }

    void Update()
    {
        if (volume != null)
        {
            // Gradually increase weight over the specified duration
            elapsedTime += Time.deltaTime;
            volume.weight = Mathf.Clamp01(elapsedTime / duration);

            Debug.Log("Current weight: " + volume.weight);
        }
    }
}
