using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class SirenLight : MonoBehaviour
{
    public Color redColor = Color.red; // Color for red light
    public Color blueColor = Color.blue; // Color for blue light
    public float intensity = 5f; // Light intensity
    public float strobeInterval = 0.5f; // Time between strobe switches
    public float startDelay = 0f; // Delay before strobing starts

    private Light sirenLight;
    private bool isStrobing = false;

    void Awake()
    {
        sirenLight = GetComponent<Light>();
        sirenLight.enabled = false; // Start with light off
    }

    void Start()
    {
        // Start strobing after the specified delay
        StartCoroutine(StartStrobeAfterDelay());
    }

    IEnumerator StartStrobeAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        sirenLight.enabled = true; // Turn the light on
        isStrobing = true;
        StartCoroutine(StrobeLight());
    }

    IEnumerator StrobeLight()
    {
        while (isStrobing)
        {
            sirenLight.color = redColor;
            sirenLight.intensity = intensity;
            yield return new WaitForSeconds(strobeInterval);

            sirenLight.color = blueColor;
            sirenLight.intensity = intensity;
            yield return new WaitForSeconds(strobeInterval);
        }
    }

    public void StopStrobe()
    {
        isStrobing = false;
        sirenLight.enabled = false; // Turn the light off
    }

    public void SetIntensity(float newIntensity)
    {
        intensity = newIntensity;
    }

    public void SetStrobeInterval(float interval)
    {
        strobeInterval = interval;
    }

    public void SetStartDelay(float delay)
    {
        startDelay = delay;
    }
}
