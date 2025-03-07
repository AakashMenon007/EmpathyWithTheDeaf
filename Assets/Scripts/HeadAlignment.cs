using UnityEngine;
using RootMotion.FinalIK; // Ensure Final IK namespace is included

public class FixedHeadTarget : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The VRIK component attached to the player.")]
    public VRIK vrik;

    [Tooltip("The target position for the head (e.g., a bed or pillow position).")]
    public Transform headTargetPosition;

    [Tooltip("The camera to be fixed at the head target position.")]
    public Transform cameraSection; // Add a public field for the camera to be assigned in Unity editor

    private Transform vrCamera;

    private void Start()
    {
        // Automatically find the VR camera if the cameraSection is not set
        if (cameraSection == null)
        {
            vrCamera = Camera.main.transform;
            if (vrCamera == null)
            {
                Debug.LogError("VR Camera reference is missing! Make sure the main camera is tagged as 'MainCamera'.");
                return;
            }
        }
        else
        {
            vrCamera = cameraSection; // Use the provided cameraSection transform
        }

        // Ensure all references are assigned
        if (vrik == null)
        {
            Debug.LogError("VRIK reference is missing! Please assign the VRIK component.");
            return;
        }

        if (headTargetPosition == null)
        {
            Debug.LogError("Head target position is missing! Please assign a target transform.");
            return;
        }

        // Fix the head target to the specified position
        AlignHeadToTarget();
    }

    private void AlignHeadToTarget()
    {
        // Move the VRIK head target to the specified position
        vrik.solver.spine.headTarget = headTargetPosition;

        // Align the VR camera to match the head target's position and rotation
        if (vrCamera != null)
        {
            vrCamera.position = headTargetPosition.position;
            vrCamera.rotation = headTargetPosition.rotation;
        }
    }

    private void Update()
    {
        // Continuously align the VR camera to ensure it matches the head target
        if (vrCamera != null)
        {
            vrCamera.position = headTargetPosition.position;
            vrCamera.rotation = headTargetPosition.rotation;
        }
    }
}
