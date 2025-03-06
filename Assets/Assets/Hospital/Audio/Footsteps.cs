using UnityEngine;
using Oculus.Haptics;
using System.Collections;

public class FootstepHaptics : MonoBehaviour
{
    [SerializeField] private HapticClip footstepClip; // Haptic clip for footsteps
    private HapticClipPlayer leftClipPlayer;
    private HapticClipPlayer rightClipPlayer;

    [SerializeField] private float walkThreshold = 0.1f; // Minimum distance to detect walking
    [SerializeField] private float stepInterval = 0.5f; // Time interval between each footstep
    private Vector3 lastPosition;
    private float timeSinceLastStep;

    void Start()
    {
        // Initialize haptic clip players
        leftClipPlayer = new HapticClipPlayer(footstepClip);
        rightClipPlayer = new HapticClipPlayer(footstepClip);

        lastPosition = transform.position;
        timeSinceLastStep = 0f;
    }

    void Update()
    {
        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // Increment the time since the last step
        timeSinceLastStep += Time.deltaTime;

        // If the distance moved exceeds the threshold and enough time has passed, trigger haptic feedback
        if (distanceMoved > walkThreshold && timeSinceLastStep >= stepInterval)
        {
            PlayFootstepHaptic();
            timeSinceLastStep = 0f; // Reset the step timer
        }

        lastPosition = transform.position;
    }

    private void PlayFootstepHaptic()
    {
        // Alternate the haptic feedback between left and right controllers
        if (Random.value > 0.5f)
        {
            leftClipPlayer.Play(Controller.Left);
            Debug.Log("Playing footstep haptic on the left hand.");
        }
        else
        {
            rightClipPlayer.Play(Controller.Right);
            Debug.Log("Playing footstep haptic on the right hand.");
        }
    }

    void OnDestroy()
    {
        // Dispose of the clip players
        leftClipPlayer?.Dispose();
        rightClipPlayer?.Dispose();
    }

    void OnApplicationQuit()
    {
        // Dispose of the haptics instance when the application quits
        Haptics.Instance.Dispose();
    }
}
