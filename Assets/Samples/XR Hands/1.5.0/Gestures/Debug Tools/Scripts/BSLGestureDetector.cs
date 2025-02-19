using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.SubsystemsImplementation;
using System.IO;

public class BSLGestureRecorder : MonoBehaviour
{
    private XRHandSubsystem handSubsystem;
    public XRHandJointID[] trackedJoints;
    public string saveFileName = "bsl_gestures.json";

    private Dictionary<string, Dictionary<XRHandJointID, Vector3>> recordedGestures = new Dictionary<string, Dictionary<XRHandJointID, Vector3>>();

    void Start()
    {
        List<XRHandSubsystem> handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);
        if (handSubsystems.Count > 0)
        {
            handSubsystem = handSubsystems[0];
        }

        LoadGestures();
    }

    void Update()
    {
        if (handSubsystem == null || !handSubsystem.running) return;

        if (Input.GetKeyDown(KeyCode.R))  // Press 'R' to save a gesture
        {
            RecordGesture("A");  // Change to the correct gesture name
        }
    }

    public void RecordGesture(string gestureName)
    {
        if (handSubsystem == null) return;
        XRHand leftHand = handSubsystem.leftHand;

        if (!leftHand.isTracked) return;

        Dictionary<XRHandJointID, Vector3> jointPositions = new Dictionary<XRHandJointID, Vector3>();

        foreach (var jointID in trackedJoints)
        {
            XRHandJoint handJoint = leftHand.GetJoint(jointID);
            if (handJoint.TryGetPose(out Pose jointPose))
            {
                jointPositions[jointID] = jointPose.position;
            }
        }

        recordedGestures[gestureName] = jointPositions;
        SaveGestures();
        Debug.Log($"Gesture '{gestureName}' recorded!");
    }

    private void SaveGestures()
    {
        // Convert the recorded gestures to a serializable structure
        string json = JsonUtility.ToJson(new GestureDataWrapper(recordedGestures), true);
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, json);
        Debug.Log("Gestures saved to: " + Application.persistentDataPath + "/" + saveFileName);
    }

    private void LoadGestures()
    {
        string path = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GestureDataWrapper data = JsonUtility.FromJson<GestureDataWrapper>(json);

            recordedGestures = new Dictionary<string, Dictionary<XRHandJointID, Vector3>>();

            // Convert List<Gesture> to Dictionary<string, Dictionary<XRHandJointID, Vector3>>
            foreach (var gesture in data.gestures)
            {
                Dictionary<XRHandJointID, Vector3> jointPositions = new Dictionary<XRHandJointID, Vector3>();
                foreach (var joint in gesture.jointPositions)
                {
                    jointPositions[joint.jointID] = joint.position;
                }
                recordedGestures[gesture.name] = jointPositions;
            }

            Debug.Log("Loaded gestures from file.");
        }
    }


    public bool CheckGesture(XRHand hand, Dictionary<XRHandJointID, Vector3> savedGesture)
    {
        foreach (var joint in savedGesture)
        {
            XRHandJoint handJoint = hand.GetJoint(joint.Key);

            if (handJoint.trackingState == XRHandJointTrackingState.None)
                return false;

            if (handJoint.TryGetPose(out Pose jointPose))
            {
                if (Vector3.Distance(jointPose.position, joint.Value) > 0.02f)  // Adjust threshold
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

// Custom wrapper class for serializing gestures
[System.Serializable]
public class GestureDataWrapper
{
    public List<Gesture> gestures;

    public GestureDataWrapper(Dictionary<string, Dictionary<XRHandJointID, Vector3>> gestures)
    {
        this.gestures = new List<Gesture>();
        foreach (var gesture in gestures)
        {
            this.gestures.Add(new Gesture(gesture.Key, gesture.Value));
        }
    }

    [System.Serializable]
    public class Gesture
    {
        public string name;
        public List<JointPosition> jointPositions;

        public Gesture(string name, Dictionary<XRHandJointID, Vector3> positions)
        {
            this.name = name;
            jointPositions = new List<JointPosition>();
            foreach (var joint in positions)
            {
                jointPositions.Add(new JointPosition(joint.Key, joint.Value));
            }
        }
    }

    [System.Serializable]
    public class JointPosition
    {
        public XRHandJointID jointID;
        public Vector3 position;

        public JointPosition(XRHandJointID jointID, Vector3 position)
        {
            this.jointID = jointID;
            this.position = position;
        }
    }
}
