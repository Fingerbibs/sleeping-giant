using UnityEngine;

public class SpotlightPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            SpotlightManager.Instance.UnlockSpotlight();
            Destroy(gameObject);
        }
    }
}
