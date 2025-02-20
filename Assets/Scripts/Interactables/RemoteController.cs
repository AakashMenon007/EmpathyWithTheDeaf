using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Assets.Scripts.Helpers;

public class RemoteController : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _xRGrabInteractable;

    public Transform raycastOrigin;
    public LayerMask layerMask;

    public string targetTag;

    private void Start()
    {
        _xRGrabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        _xRGrabInteractable.activated.AddListener(PressPowerButton);
    }

    private void Update()
    {
        Debug.DrawRay(raycastOrigin.position, transform.forward * 10f, Color.magenta);
    }

    private void PressPowerButton(ActivateEventArgs args)
    {

        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, 10f, layerMask))
        {
        Debug.Log("Pressed power button");  
            if (hit.transform.CompareTag(targetTag))
            {
                var switchable = hit.transform.gameObject.GetComponent<ISwitchable>();

                if (switchable.SwitchedOn)
                    switchable.SwitchOff();
                else
                    switchable.SwitchOn();
            }
        }
    }

    private void OnDestroy()
    {
        _xRGrabInteractable.activated.RemoveAllListeners();
    }
}
