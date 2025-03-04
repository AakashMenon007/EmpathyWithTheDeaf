using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HapticFeedback : MonoBehaviour
{
    public float intensity = 0.7f;
    public float duration = 0.1f;

    private void Start()
    {


    }


    public void TriggerHaptic(XRBaseInteractor interactor)
    {
        if (interactor is XRBaseInputInteractor controllerInteractor)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand); // or LeftHand
            if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities))
            {
                Debug.Log($"Haptic Capabilities: {capabilities.supportsImpulse}");
            }
            else
            {
                Debug.LogError("Failed to get haptic capabilities");
            }

            controllerInteractor.SendHapticImpulse(intensity, duration);
            Debug.Log("Haptic feedback triggered");
        }
    }
}
