using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;
using HapticPatterns.Input;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace HapticPatterns.Samples
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class TactileTriggerPattern : MonoBehaviour
    {
        public HapticPattern gradualPattern;
        public HapticPatternVisualiser patternVisualiser;
        [Space]
        public Animator pistolAnimator;

        private XRIDefaultInputActions _input;
        private XRGrabInteractable _grabInteractable;
        
        private void Start()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _input = new XRIDefaultInputActions();
            _input.Enable();
            
            patternVisualiser.pattern = gradualPattern;
        }

        private void Update()
        {
            if (!_grabInteractable.isSelected)
                return;
            if (_grabInteractable.interactorsSelecting[0] is not XRDirectInteractor)
                return;
            
            float trigger01 = GetTriggerValue();
            
            //Clamp between 0, .99, 1 would overflow back to 0
            pistolAnimator.SetFloat("Trigger", Mathf.Clamp(trigger01, 0, .99f));
            if(trigger01 > 0.001f)
                //Progressively play pattern, with change in trigger value over Time.deltaTime (until next frame)
                gradualPattern.PlayGradually(_grabInteractable, trigger01);
        
            patternVisualiser.pointOnTimeline = trigger01;
        }

        private float GetTriggerValue()
        {
            float leftTrigger = _input.XRILeftHandInteraction.ActivateValue.ReadValue<float>();
            float rightTrigger = _input.XRIRightHandInteraction.ActivateValue.ReadValue<float>();

            //Just return the largest value on both hands, since the awful new input system doesn't
            //document how to access input on only the selecting controller ):
            //TODO improve once i figure out how to do it without being overly complex
            return Mathf.Max(leftTrigger, rightTrigger);  
        }
    }
}
