using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float maxDistance;
    private float rightStickX;
    private float rightStickY;

    // Update is called once per frame
    void Update(){
        rightStickX = Input.GetAxis("RightStickHorizontal");
        rightStickY = Input.GetAxis("RightStickVertical");

         // Create the movement vector
        Vector3 movement = new Vector3(rightStickY, 0f, rightStickX);
        // Rotate the movement vector by 45 degrees for an isometric perspective
        Vector3 rotatedMovement = new Vector3(movement.x - movement.z, 0f, movement.x + movement.z); // Isometric rotation matrix: X' = (X - Y), Z' = (X + Y)
        // Apply the movement to the cursor
        transform.Translate(rotatedMovement * speed * Time.deltaTime, Space.World);

        ClampCursorToPlayer();
    }

    void ClampCursorToPlayer(){
        Vector3 objectPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosXZ = new Vector3(player.position.x, 0, player.position.z);

        // Calculate distance in the XZ plane
        float distance = Vector3.Distance(objectPosXZ, playerPosXZ);

        // Check if the object is outside the radius
        if (distance > maxDistance){
            // Calculate direction in XZ plane
            Vector3 direction = (objectPosXZ - playerPosXZ).normalized;

            // Clamp position in XZ, but keep original Y
            Vector3 clampedPos = player.position + direction * maxDistance;
            transform.position = new Vector3(clampedPos.x, transform.position.y, clampedPos.z);
        }
    }

}
