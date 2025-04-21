using UnityEngine;
using System;

public class SpotlightManager : MonoBehaviour
{
    public static SpotlightManager Instance;

    public bool HasSpotlightUnlocked { get; private set; } = false;
    public bool IsSpotlightOn { get; private set; } = false;

    public event Action OnSpotlightUnlocked;
    public event Action<bool> OnSpotlightToggled;

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UnlockSpotlight(){
        if (HasSpotlightUnlocked) return;

        HasSpotlightUnlocked = true;
        Debug.Log("Spotlight unlocked!");
        OnSpotlightUnlocked?.Invoke();
    }

    public void ToggleSpotlight(){
        if (!HasSpotlightUnlocked) return;

        IsSpotlightOn = !IsSpotlightOn;
        OnSpotlightToggled?.Invoke(IsSpotlightOn);

        if (IsSpotlightOn)
            AudioManager.Instance.PlaySpotlightOn();
        else
            AudioManager.Instance.PlaySpotlightOff();
    }
}
