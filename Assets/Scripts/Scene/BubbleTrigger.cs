using UnityEngine;

public class BubbleTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            // Use the custom SceneLoader to load the new scene
            if (SceneLoader.Instance != null)
                SceneLoader.Instance.LoadScene(sceneToLoad);
            else
                Debug.LogWarning("SceneLoader instance not found!");
        }
    }
}
