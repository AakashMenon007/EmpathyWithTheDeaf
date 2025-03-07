using UnityEngine;

public class FixedBodyPartPositions : MonoBehaviour
{
    [Header("References")]
    public Transform vrCamera; // Main Camera of XR Rig
    public Transform headTarget; // VRIK Head Target
    public Transform rightHandTarget; // VRIK Right Hand Target
    public Transform leftHandTarget; // VRIK Left Hand Target

    [Header("Fixed Body Part Positions")]
    public Transform fixedHeadPosition; // Empty GameObject for head position
    public Transform fixedHipsPosition; // Empty GameObject for hips position
    public Transform fixedRightLegPosition; // Empty GameObject for right leg position
    public Transform fixedLeftLegPosition; // Empty GameObject for left leg position

    private void LateUpdate()
    {
        // Lock body parts to their fixed positions
        if (fixedHeadPosition != null)
        {
            headTarget.position = fixedHeadPosition.position;
            headTarget.rotation = fixedHeadPosition.rotation;
        }

        if (fixedHipsPosition != null)
        {
            transform.position = fixedHipsPosition.position;
            transform.rotation = fixedHipsPosition.rotation;
        }

        if (fixedRightLegPosition != null)
        {
            // Example: You can assign this to a VRIK leg target if applicable
            // RightLegTarget.position = fixedRightLegPosition.position;
            // RightLegTarget.rotation = fixedRightLegPosition.rotation;
        }

        if (fixedLeftLegPosition != null)
        {
            // Example: You can assign this to a VRIK leg target if applicable
            // LeftLegTarget.position = fixedLeftLegPosition.position;
            // LeftLegTarget.rotation = fixedLeftLegPosition.rotation;
        }

        // Adjust head target to follow VR camera's rotation but stay locked to the bed
        if (vrCamera != null)
        {
            headTarget.rotation = vrCamera.rotation;
        }
    }
}
