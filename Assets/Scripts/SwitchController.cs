using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public ParticleSystem steamParticleSystem;
    public FireAlarmController fireAlarm; // Reference to the separate fire alarm script

    // Public variables for delay times
    public float steamDelay = 3f;   // Time delay before steam starts
    public float alarmDelay = 10f;  // Time delay before the fire alarm starts

    public bool switchOn = false;
    private Coroutine steamCoroutine;
    private Coroutine alarmCoroutine;

    void Start()
    {
        fireParticleSystem.Stop();
        steamParticleSystem.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        ToggleSwitch();
    }

    private XRKnob knob;

    public void ToggleSwitch()
    {
        switchOn = !switchOn;

        if (switchOn && knob.value == 1)
        {
            fireParticleSystem.Play();

            if (steamCoroutine != null) StopCoroutine(steamCoroutine);
            steamCoroutine = StartCoroutine(ActivateSteamAfterDelay(steamDelay));

            if (alarmCoroutine != null) StopCoroutine(alarmCoroutine);
            alarmCoroutine = StartCoroutine(StartFireAlarmAfterDelay(alarmDelay));
        }
        else
        {
            fireParticleSystem.Stop();
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
                fireAlarm.StopAlarm(); // Stop the fire alarm (blinking light) if running
            }
        }
    }

    IEnumerator ActivateSteamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        steamParticleSystem.Play();
        Debug.Log("Kettle Steam activated");
    }

    IEnumerator StartFireAlarmAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (fireAlarm != null)
        {
            fireAlarm.StartAlarm(); // Trigger the blinking red light alarm
            Debug.Log("Fire Alarm activated");
        }
    }
}
