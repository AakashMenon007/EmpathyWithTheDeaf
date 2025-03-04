using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem; // Fire particle effect
    public ParticleSystem steamParticleSystem; // Steam particle effect
    public GameObject fireAlarm;      // Fire alarm controller
    public UnityEngine.XR.Content.Interaction.XRKnob knob;                        // Reference to your XRKnob
    public float steamDelay = 3f;                // Delay before steam activates
    public float alarmDelay = 10f;               // Delay before the fire alarm starts
    public bool flagSteamOn = false;             // Set to true when the knob is turned on

    public GameObject fireLight; 

    private Coroutine steamCoroutine;
    private Coroutine alarmCoroutine;

    void Start()
    {
        // Ensure particle systems are off initially.
        if (fireParticleSystem != null)
            fireParticleSystem.Stop();
        if (steamParticleSystem != null)
            steamParticleSystem.Stop();


        // Initialize the switch state.
        ToggleSwitch(knob.value);
    }

    // This method is linked to the XRKnob's onValueChange event.
    public void ToggleSwitch(float knobValue)
    {
        if (knobValue >= 0.50f)
        {
            if (!flagSteamOn)
            {
                flagSteamOn = true;
                if (fireParticleSystem != null)
                {
                    fireParticleSystem.Play();
                    fireLight.SetActive(true);
                }

                if (steamCoroutine != null)
                    StopCoroutine(steamCoroutine);
                steamCoroutine = StartCoroutine(ActivateSteamAfterDelay());

                if (alarmCoroutine != null)
                    StopCoroutine(alarmCoroutine);
                alarmCoroutine = StartCoroutine(StartAlarmAfterDelay());
            }
        }
        else
        {
            flagSteamOn = false;
            if (fireParticleSystem != null)
            {
                fireParticleSystem.Stop();
                fireLight.SetActive(true);
            }

            if (steamParticleSystem != null)
                steamParticleSystem.Stop();
            if (steamCoroutine != null)
            {
                StopCoroutine(steamCoroutine);
                steamCoroutine = null;
            }
            if (alarmCoroutine != null)
            {
                StopCoroutine(alarmCoroutine);
                alarmCoroutine = null;
            }
            if (fireAlarm != null)
            {
                fireAlarm.SetActive(false);
            }
        }
    }

    IEnumerator ActivateSteamAfterDelay()
    {
        yield return new WaitForSeconds(steamDelay);
        if (steamParticleSystem != null)
            steamParticleSystem.Play();
    }

    IEnumerator StartAlarmAfterDelay()
    {
        yield return new WaitForSeconds(alarmDelay);
        if (fireAlarm != null)
            fireAlarm.SetActive(true);
    }
}