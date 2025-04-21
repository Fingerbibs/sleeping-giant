using UnityEngine;
using System.Collections;
using System;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    private Light areaLight;

    public float fadeDuration = 1f;  // Duration of the fade
    public float targetIntensity = 300f;  // Target intensity to fade to

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Method to fade the light in or out
    public void FadeLights(bool fadeIn, Light lightSource)
    {
        areaLight = lightSource;
        StopAllCoroutines();  // Stop any existing fades
        StartCoroutine(FadeLightsCoroutine(fadeIn ? targetIntensity : 0f));  // Fade to target intensity or off
    }

    private IEnumerator FadeLightsCoroutine(float target)
    {
        if (areaLight == null) yield break;  // Ensure the light source is assigned

        float initialIntensity = areaLight.intensity;
        float timeElapsed = 0f;

        // Fade the light's intensity over time
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float lerpedIntensity = Mathf.Lerp(initialIntensity, target, timeElapsed / fadeDuration);
            areaLight.intensity = lerpedIntensity;
            yield return null;
        }

        // Ensure the light reaches the exact target intensity at the end
        areaLight.intensity = target;
    }
}