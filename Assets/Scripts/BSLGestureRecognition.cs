using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using TMPro;

public class BSLGestureRecognition : MonoBehaviour
{
    private XRHandSubsystem handSubsystem;
    public TMP_Text recognizedText; // Assign your TextMeshPro UI element in the Inspector

    private Dictionary<string, Dictionary<XRHandJointID, Vector3>> bslGestures = new Dictionary<string, Dictionary<XRHandJointID, Vector3>>();

    void Start()
    {
        // Get the XR Hand Subsystem
        List<XRHandSubsystem> handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);
        if (handSubsystems.Count > 0)
        {
            handSubsystem = handSubsystems[0];
        }

        // Load predefined BSL gestures
        LoadBSLAlphabetGestures();
    }

    void Update()
    {
        if (handSubsystem == null || !handSubsystem.running) return;

        string recognizedGesture = RecognizeGesture();
        if (!string.IsNullOrEmpty(recognizedGesture))
        {
            recognizedText.text = recognizedGesture; // Display the recognized letter
        }
    }

    void LoadBSLAlphabetGestures()
    {
        // NOTE: These positions should be calibrated with actual tracking data.
        bslGestures["A"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["B"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.2f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.1f, 0.3f, 0.0f) }
        };

        bslGestures["C"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(-0.05f, 0.0f, 0.0f) }
        };

        bslGestures["D"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["E"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(-0.02f, -0.01f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.01f, -0.02f, 0.0f) }
        };

        bslGestures["F"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["G"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["H"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.2f, 0.0f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.15f, 0.0f, 0.0f) }
        };

        bslGestures["I"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["J"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.2f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.1f, 0.3f, 0.0f) }
        };

        bslGestures["K"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(-0.05f, 0.0f, 0.0f) }
        };

        bslGestures["L"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["M"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(-0.02f, -0.01f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.01f, -0.02f, 0.0f) }
        };

        bslGestures["N"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["O"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["P"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.2f, 0.0f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.15f, 0.0f, 0.0f) }
        };
        bslGestures["Q"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["R"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.2f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.1f, 0.3f, 0.0f) }
        };

        bslGestures["S"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(-0.05f, 0.0f, 0.0f) }
        };

        bslGestures["T"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.0f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.0f, -0.05f, 0.0f) }
        };

        bslGestures["U"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(-0.02f, -0.01f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.01f, -0.02f, 0.0f) }
        };

        bslGestures["V"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.05f, 0.05f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["W"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["X"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.2f, 0.0f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.15f, 0.0f, 0.0f) }
        };
        bslGestures["Y"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.1f, 0.0f, 0.0f) },
            { XRHandJointID.ThumbTip, new Vector3(0.05f, -0.05f, 0.0f) }
        };

        bslGestures["Z"] = new Dictionary<XRHandJointID, Vector3>
        {
            { XRHandJointID.IndexTip, new Vector3(0.2f, 0.0f, 0.0f) },
            { XRHandJointID.MiddleTip, new Vector3(0.15f, 0.0f, 0.0f) }
        };
    }

    string RecognizeGesture()
    {
        if (handSubsystem == null) return "";

        XRHand leftHand = handSubsystem.leftHand;
        if (!leftHand.isTracked) return "";

        foreach (var gesture in bslGestures)
        {
            if (CheckGesture(leftHand, gesture.Value))
            {
                return gesture.Key; // Return recognized letter
            }
        }

        return ""; // No gesture matched
    }

    bool CheckGesture(XRHand hand, Dictionary<XRHandJointID, Vector3> savedGesture)
    {
        foreach (var joint in savedGesture)
        {
            XRHandJoint handJoint = hand.GetJoint(joint.Key);

            if (handJoint.trackingState == XRHandJointTrackingState.None)
                return false;

            if (handJoint.TryGetPose(out Pose jointPose))
            {
                if (Vector3.Distance(jointPose.position, joint.Value) > 0.02f) // Adjust threshold
                    return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
