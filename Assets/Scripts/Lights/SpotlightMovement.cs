using UnityEngine;

public class SpotlightMovement : MonoBehaviour
{
    public Transform cursor;
    public float heightAbove = 500f;
    private float offset = 4.2f;

    void Update(){
        if(cursor == null)
            return;
        
        Vector3 newPos = new Vector3(cursor.position.x, cursor.position.y + heightAbove, cursor.position.z-offset);
        transform.position = newPos;
        transform.rotation = Quaternion.Euler(75f, 0f, 75f);

        
    }
}
