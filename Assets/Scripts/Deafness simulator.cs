using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class DeafnessSimulatorListener : MonoBehaviour
{
    [Header("Simulation Settings")]
    public float duration = 10f;      // Duration to fully simulate deafness
    public bool startSimulation = false; // Toggle to start the simulation

    [Header("Distortion Settings")]
    public float maxDistortion = 0.8f;

    [Header("Low-Pass Filter Settings")]
    public float minCutoffFrequency = 300f;

    [Header("Echo Settings")]
    public float echoDelay = 500f;
    public float echoDecay = 0.5f;

    [Header("Audio Setup")]
    public AudioListener targetAudioListener; // Drag and drop the AudioListener here

    private AudioDistortionFilter distortionFilter;
    private AudioLowPassFilter lowPassFilter;
    private AudioEchoFilter echoFilter;
    private float elapsedTime = 0f;

    void Start()
    {
        if (targetAudioListener == null)
        {
            Debug.LogError("No AudioListener assigned! Please drag and drop an AudioListener in the Inspector.");
            return;
        }

        // Attach audio filters to the target AudioListener
        GameObject listenerObject = targetAudioListener.gameObject;

        distortionFilter = listenerObject.AddComponent<AudioDistortionFilter>();
        lowPassFilter = listenerObject.AddComponent<AudioLowPassFilter>();
        echoFilter = listenerObject.AddComponent<AudioEchoFilter>();

        // Initialize filters
        distortionFilter.distortionLevel = 0f;
        lowPassFilter.cutoffFrequency = 22000f;
        echoFilter.delay = 0f;
        echoFilter.decayRatio = 0f;
    }

    void Update()
    {
        if (startSimulation)
        {
            if (targetAudioListener == null)
            {
                Debug.LogWarning("AudioListener not assigned. Simulation cannot proceed.");
                startSimulation = false;
                return;
            }

            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);

            // Distortion Effect
            distortionFilter.distortionLevel = Mathf.Lerp(0f, maxDistortion, progress);

            // Low-Pass Filter (Muffling Effect)
            lowPassFilter.cutoffFrequency = Mathf.Lerp(22000f, minCutoffFrequency, progress);

            // Echo Effect (Disorientation)
            echoFilter.delay = Mathf.Lerp(0f, echoDelay, progress);
            echoFilter.decayRatio = Mathf.Lerp(0f, echoDecay, progress);

            // Debugging Output
            Debug.Log($"Progress: {progress} | Distortion: {distortionFilter.distortionLevel} | Cutoff Frequency: {lowPassFilter.cutoffFrequency} | Echo Delay: {echoFilter.delay}");

            // Stop simulation when fully deaf
            if (progress >= 1f)
            {
                startSimulation = false;
                Debug.Log("Deafness Simulation Complete.");
            }
        }
    }

    // Public method to trigger the simulation
    public void StartDeafnessSimulation()
    {
        if (targetAudioListener == null)
        {
            Debug.LogError("No AudioListener assigned! Cannot start simulation.");
            return;
        }

        startSimulation = true;
        elapsedTime = 0f;
        Debug.Log("Deafness Simulation Started.");
    }
}
