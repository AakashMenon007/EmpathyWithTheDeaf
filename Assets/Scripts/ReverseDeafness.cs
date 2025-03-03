using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(AudioListener))]
public class ReverseDeafnessSimulatorListener : MonoBehaviour
{
    [Header("Simulation Settings")]
    public float duration = 10f;      // Duration for the transition
    public bool startSimulation = false; // Toggle to start the deafness simulation
    private bool reverseSimulation = false; // Toggle to reverse the simulation

    [Header("Distortion Settings")]
    public float maxDistortion = 0.8f;

    [Header("Low-Pass Filter Settings")]
    public float minCutoffFrequency = 300f;

    [Header("Echo Settings")]
    public float echoDelay = 500f;
    public float echoDecay = 0.5f;

    [Header("Audio Setup")]
    public AudioListener targetAudioListener; // Drag and drop the AudioListener here

    [Header("XR Hearing Aids")]
    public XRSocketInteractor leftHearingAidSocket; // Left hearing aid socket
    public XRSocketInteractor rightHearingAidSocket; // Right hearing aid socket

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

        // Initialize filters to simulate deafness initially
        distortionFilter.distortionLevel = maxDistortion;
        lowPassFilter.cutoffFrequency = minCutoffFrequency;
        echoFilter.delay = echoDelay;
        echoFilter.decayRatio = echoDecay;
    }

    void Update()
    {
        // Check if both hearing aids are attached
        if (!reverseSimulation && AreBothHearingAidsAttached())
        {
            ReverseDeafnessSimulation();
        }

        if (startSimulation)
        {
            RunSimulation(false);
        }
        else if (reverseSimulation)
        {
            RunSimulation(true);
        }
    }

    private void RunSimulation(bool isReversing)
    {
        if (targetAudioListener == null)
        {
            Debug.LogWarning("AudioListener not assigned. Simulation cannot proceed.");
            startSimulation = reverseSimulation = false;
            return;
        }

        elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsedTime / duration);

        if (isReversing)
        {
            // Reverse simulation (deaf to hearing)
            distortionFilter.distortionLevel = Mathf.Lerp(maxDistortion, 0f, progress);
            lowPassFilter.cutoffFrequency = Mathf.Lerp(minCutoffFrequency, 22000f, progress);
            echoFilter.delay = Mathf.Lerp(echoDelay, 0f, progress);
            echoFilter.decayRatio = Mathf.Lerp(echoDecay, 0f, progress);
        }
        else
        {
            // Normal simulation (hearing to deaf)
            distortionFilter.distortionLevel = Mathf.Lerp(0f, maxDistortion, progress);
            lowPassFilter.cutoffFrequency = Mathf.Lerp(22000f, minCutoffFrequency, progress);
            echoFilter.delay = Mathf.Lerp(0f, echoDelay, progress);
            echoFilter.decayRatio = Mathf.Lerp(0f, echoDecay, progress);
        }

        Debug.Log($"Progress: {progress} | Distortion: {distortionFilter.distortionLevel} | Cutoff Frequency: {lowPassFilter.cutoffFrequency} | Echo Delay: {echoFilter.delay}");

        if (progress >= 1f)
        {
            startSimulation = reverseSimulation = false;
            Debug.Log(isReversing ? "Hearing Restoration Complete." : "Deafness Simulation Complete.");
        }
    }

    private bool AreBothHearingAidsAttached()
    {
        // Use hasSelection to check if there is any active selection on the interactors
        bool isLeftAttached = leftHearingAidSocket.hasSelection;
        bool isRightAttached = rightHearingAidSocket.hasSelection;

        return isLeftAttached && isRightAttached;
    }


    public void ReverseDeafnessSimulation()
    {
        if (targetAudioListener == null)
        {
            Debug.LogError("No AudioListener assigned! Cannot reverse simulation.");
            return;
        }

        reverseSimulation = true;
        startSimulation = false;
        elapsedTime = 0f;
        Debug.Log("Hearing Restoration Simulation Started.");
    }
}
