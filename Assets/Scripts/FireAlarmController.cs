using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarmController : MonoBehaviour
{
    public Light alarmLight;         // The red alarm light
    public float blinkInterval = 0.5f; // How fast the light blinks
    private float timer = 0f;          // Timer to track the blink interval

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > blinkInterval)
        {
            alarmLight.enabled = !alarmLight.enabled;
            timer = 0f;
        }
    }
}
