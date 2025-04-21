using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    void Start(){
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update(){
        Vector3 movement = transform.position - lastPosition;
        movement.y = 0; // Ignore vertical movement (jumping/falling)

        float speed = movement.magnitude / Time.deltaTime;
        animator.SetFloat("speed", speed);

        lastPosition = transform.position;

    }
}
