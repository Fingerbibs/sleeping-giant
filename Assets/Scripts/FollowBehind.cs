using UnityEngine;

public class FollowBehind : MonoBehaviour
{
    public Transform target;       // Assign the Giant
    public float distanceBehind = 2.0f;
    public float heightOffset = 0.0f;
    public float smoothSpeed = 10f; // For smooth follow (optional)

    void Update(){
        if (target == null) return;

        // Calculate position behind the target based on its forward direction
        Vector3 desiredPosition = target.position - target.forward * distanceBehind + Vector3.up * heightOffset;

        // Optional: smooth movement
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
  
}
