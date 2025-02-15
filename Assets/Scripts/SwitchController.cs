using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour
{
    public ParticleSystem fireParticleSystem; // Fire particle system
    public ParticleSystem steamParticleSystem; // Steam particle system
    public Material offMaterial;
    public Material onMaterial;

    private Renderer switchRenderer;
    private bool switchOn = false;
    private Coroutine steamCoroutine;

    void Start()
    {
        switchRenderer = GetComponent<Renderer>();
        switchRenderer.material = offMaterial;
        fireParticleSystem.Stop();
        steamParticleSystem.Stop(); // Ensure steam is off initially
    }

    void OnTriggerEnter(Collider other)
    {
        ToggleSwitch();
    }

    void ToggleSwitch()
    {
        switchOn = !switchOn;

        if (switchOn)
        {
            switchRenderer.material = onMaterial;
            fireParticleSystem.Play();

            // Start steam after 5 seconds
            if (steamCoroutine != null) StopCoroutine(steamCoroutine);
            steamCoroutine = StartCoroutine(ActivateSteamAfterDelay(5f));
        }
        else
        {
            switchRenderer.material = offMaterial;
            fireParticleSystem.Stop();
            steamParticleSystem.Stop(); // Stop steam immediately

            if (steamCoroutine != null)
            {
                StopCoroutine(steamCoroutine);
                steamCoroutine = null;
            }
        }
    }

    IEnumerator ActivateSteamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        steamParticleSystem.Play(); // Start steam after delay
    }
}