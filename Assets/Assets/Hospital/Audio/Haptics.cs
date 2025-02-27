using UnityEngine;
using Oculus.Haptics;
using System.Collections;

public class AutoHapticsPlaySingleClip : MonoBehaviour
{
    [SerializeField] private HapticClip clip; // Haptic clip to play
    private HapticClipPlayer leftClipPlayer;
    private HapticClipPlayer rightClipPlayer;

    private float delayBeforePlay = 2f; // Delay before starting the haptic feedback in seconds

    void Start()
    {
        // Initialize the clip players for both hands
        leftClipPlayer = new HapticClipPlayer(clip);
        rightClipPlayer = new HapticClipPlayer(clip);

        // Start the haptic feedback with a delay
        StartCoroutine(PlayHapticWithDelay());
    }

    private IEnumerator PlayHapticWithDelay()
    {
        // Wait for the specified delay before starting the haptic feedback
        yield return new WaitForSeconds(delayBeforePlay);

        // Play the haptic feedback on both hands
        leftClipPlayer.Play(Controller.Left);
        rightClipPlayer.Play(Controller.Right);
        Debug.Log("Playing haptic feedback on both hands after delay.");
    }

    // Cleanup on destroying the object
    void OnDestroy()
    {
        leftClipPlayer?.Dispose();
        rightClipPlayer?.Dispose();
    }

    // Cleanup when the application quits
    void OnApplicationQuit()
    {
        Haptics.Instance.Dispose();
    }
}
