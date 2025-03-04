using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarmController : MonoBehaviour
{
    public Light alarmLight;         // The red alarm light
    public float blinkInterval = 0.5f; // How fast the light blinks

    private Coroutine alarmCoroutine;

    public void StartAlarm()
    {
        if (alarmCoroutine == null)
        {
            alarmCoroutine = StartCoroutine(BlinkAlarm());
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
            alarmLight.enabled = false;
    }

    IEnumerator BlinkAlarm()
    {
        while (true)
        {
            if (alarmLight != null)
                alarmLight.enabled = !alarmLight.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
