using System.Collections;
using System.Collections.Generic;
using HapticPatterns;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace HapticPatterns
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class ExampleOverTime : MonoBehaviour
    {
        public HapticPattern pattern;

        public void SomeEvent()
        {
            XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();

            pattern.PlayOverTime(interactable);
        }
    }
}