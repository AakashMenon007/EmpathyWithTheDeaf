using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace HapticPatterns
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class ExampleGradual : MonoBehaviour
    {
        public HapticPattern pattern;
        public float gradualValue01;

        void Update()
        {
            GetComponent<ExampleOverTime>().SomeEvent();
            XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();

            pattern.PlayGradually(interactable, gradualValue01);
        }
    }
}