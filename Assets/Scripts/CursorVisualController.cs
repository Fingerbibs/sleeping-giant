using UnityEngine;

public class CursorVisualController : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void Start() {
        if (SpotlightManager.Instance != null) {
            SpotlightManager.Instance.OnSpotlightUnlocked += ShowCursor;
        }
    }

    private void OnDisable(){
        if (SpotlightManager.Instance != null)
            SpotlightManager.Instance.OnSpotlightUnlocked -= ShowCursor;
    }

    private void ShowCursor()
    {
        meshRenderer.enabled = true;
    }
}
