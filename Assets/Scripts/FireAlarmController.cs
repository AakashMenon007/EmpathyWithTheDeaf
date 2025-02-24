using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarmController : MonoBehaviour
{
    public Light alarmLight; // Assign a red light in the inspector
    public float blinkInterval = 0.5f; // Time interval for blinking

    private Coroutine alarmCoroutine;

    public void StartAlarm()
    {
        if (alarmCoroutine == null)
        {
            alarmCoroutine = StartCoroutine(BlinkAlarmLight());
        }
    }

    public void StopAlarm()
    {
        if (alarmCoroutine != null)
        {
            StopCoroutine(alarmCoroutine);
            alarmCoroutine = null;
        }

        if (alarmLight != null)
        {
            alarmLight.enabled = false; // Ensure light is off when stopping
        }
    }

    private IEnumerator BlinkAlarmLight()
    {
        while (true)
        {
            if (alarmLight != null)
            {
                alarmLight.enabled = !alarmLight.enabled; // Toggle light
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
