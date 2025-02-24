using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public ParticleSystem steamParticleSystem;
    public FireAlarmController fireAlarm; // Reference to FireAlarmController

    private bool switchOn = false;
    private Coroutine steamCoroutine;
    private Coroutine alarmCoroutine;

    void Start()
    {
        fireParticleSystem.Stop();
        steamParticleSystem.Stop();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    ToggleSwitch();
    //}

    public void ToggleSwitch()
    {
        switchOn = !switchOn;

        if (switchOn)
        {
            fireParticleSystem.Play();

            if (steamCoroutine != null) StopCoroutine(steamCoroutine);
            steamCoroutine = StartCoroutine(ActivateSteamAfterDelay(3f));

            if (alarmCoroutine != null) StopCoroutine(alarmCoroutine);
            alarmCoroutine = StartCoroutine(StartFireAlarmAfterDelay(10f));
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
                fireAlarm.StopAlarm(); // Stop alarm if switch is turned off
            }
        }
    }

    IEnumerator ActivateSteamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        steamParticleSystem.Play();
    }

    IEnumerator StartFireAlarmAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (fireAlarm != null)
        {
            fireAlarm.StartAlarm(); // Trigger the alarm after delay
        }
    }
}
