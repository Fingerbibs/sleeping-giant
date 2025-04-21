using UnityEngine;
using System.Collections;

public class SpotlightTriggerZone : MonoBehaviour
{
    private GameObject currentTarget;
    private Animator animator;
    public Light areaLight;
    
    private bool isStanding = true;

    private IEnumerator Start(){
        yield return new WaitForSeconds(1f);
        isStanding = false;
        if(areaLight != null)
            areaLight.intensity = 0;
        Debug.Log("waiting");
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            currentTarget = other.gameObject;
            animator = currentTarget.GetComponent<Animator>(); // Get Animator

            if(!isStanding)
                animator?.SetTrigger("toGiant");
                
            if(areaLight != null)
                LightManager.Instance.FadeLights(true, areaLight);
            // Trigger Camera movement
            EventManager.TriggerEvent("ToggleCamera");

            Debug.Log($"[SpotlightTrigger] Entered: {currentTarget.name}");
        }

    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player") && other.gameObject == currentTarget){
            animator?.SetTrigger("toSoul");

            EventManager.TriggerEvent("ToggleCamera");

            Debug.Log($"[SpotlightTrigger] Exited: {currentTarget.name}");
            currentTarget = null;

            if(areaLight != null)
                LightManager.Instance.FadeLights(false, areaLight);
        }
    }

    public void ForceExit(){
        if (currentTarget != null){
            Debug.Log($"[SpotlightTrigger] Forcing exit for: {currentTarget.name}");

            // Simulate OnTriggerExit behavior
            animator?.SetTrigger("toSoul");
            EventManager.TriggerEvent("ToggleCamera");
            currentTarget.SendMessage("OnManualTriggerExit", SendMessageOptions.DontRequireReceiver);
            currentTarget = null;
        }
    }

}
