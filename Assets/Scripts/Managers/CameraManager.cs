using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraViewToggle cameraToggle;

    private void OnEnable(){
        // Subscribe to events or messages
        EventManager.StartListening("ToggleCamera", ToggleCameraView);
    }

    private void OnDisable(){
        // Unsubscribe to prevent memory leaks
        EventManager.StopListening("ToggleCamera", ToggleCameraView);
    }

    private void ToggleCameraView(){
        cameraToggle?.ToggleCameraView();
    }
}
