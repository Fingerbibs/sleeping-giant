using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static bool hasSpawned = false;

    void Awake(){
        if (hasSpawned){
            Destroy(gameObject);
            return;
        }

        hasSpawned = true;
        DontDestroyOnLoad(gameObject);
    }
}
