using UnityEngine;
using System.Collections;

public class CameraViewToggle : MonoBehaviour
{
    private Camera mainCamera;
    public float rotationDuration = 1f;
    public float zoomDuration = 1f;

    public float defaultZoom = 10f;
    public float zoomedInSize = 6f;
    public float startZoom = 100f;
    
    private Quaternion isometricRotation = Quaternion.Euler(35f, 45f, 0f);
    private Quaternion topDownRotation = Quaternion.Euler(90f, 0f, 0f);
    
    private bool isTopDown = true;
    private Coroutine rotationCoroutine;
    private Coroutine zoomCoroutine;

    void Start(){
        // Start in isometric view
        mainCamera = GetComponent<Camera>();
        mainCamera.transform.rotation = topDownRotation;
        mainCamera.orthographicSize = startZoom;
    }

    public void ToggleCameraView(){
        Quaternion targetRotation = isTopDown ? isometricRotation : topDownRotation;
        float targetZoom = isTopDown ? zoomedInSize : defaultZoom;

        if (rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);
        rotationCoroutine = StartCoroutine(RotateTo(targetRotation));

        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ZoomTo(targetZoom));

        isTopDown = !isTopDown;
    }

    private IEnumerator RotateTo(Quaternion targetRotation){
        Quaternion startRotation = mainCamera.transform.rotation;
        float elapsed = 0f;

        while (elapsed < rotationDuration){
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotationDuration);
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        mainCamera.transform.rotation = targetRotation;
    }

    private IEnumerator ZoomTo(float targetSize){
        float startSize = mainCamera.orthographicSize;
        float elapsed = 0f;

        while (elapsed < zoomDuration){
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / zoomDuration);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
    }
}
