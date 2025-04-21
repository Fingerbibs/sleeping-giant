using UnityEngine;

public class SpotlightToggle : MonoBehaviour
{
    public CapsuleCollider spotlightCollider;
    public SpotlightTriggerZone triggerZone;

    private Light spotlight;

    void Start(){
        spotlight = GetComponent<Light>();
        spotlight.enabled = false;

        spotlightCollider.enabled = false;

        SpotlightManager.Instance.OnSpotlightUnlocked += HandleUnlock;
        SpotlightManager.Instance.OnSpotlightToggled += HandleToggle;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            SpotlightManager.Instance.ToggleSpotlight();
        }
    }
    private void OnDestroy()
    {
        // Clean up event subscriptions
        if (SpotlightManager.Instance != null)
        {
            SpotlightManager.Instance.OnSpotlightUnlocked -= HandleUnlock;
            SpotlightManager.Instance.OnSpotlightToggled -= HandleToggle;
        }
    }

    private void HandleUnlock()
    {
        Debug.Log("SpotlightToggle: Unlocked and ready!");
    }

    private void HandleToggle(bool isOn)
    {
        spotlight.enabled = isOn;
        spotlightCollider.enabled = isOn;

        if (!isOn && triggerZone != null)
            triggerZone.ForceExit();
    }
}

