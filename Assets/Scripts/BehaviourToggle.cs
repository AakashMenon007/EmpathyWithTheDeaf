using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BehaviourToggle : MonoBehaviour
{
    public Behaviour behaviour;
    public void Toggle()
    {
        behaviour.enabled = !behaviour.enabled;
    }
}

//xRSimpleInteractable = GetComponent<XRSimpleInteractable>();

//xRSimpleInteractable.hoverEntered.AddListener(ToggleLights)
