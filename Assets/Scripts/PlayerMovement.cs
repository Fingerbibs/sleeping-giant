using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public bool isOnGround = false;

    public Camera mainCamera;

    private float horizontalInput;
    private float forwardInput;
    private Animator animator;
    private Rigidbody playerRb;

    void Start(){
        //setting our rigid body to the component in the Inspector
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        StartCoroutine(StandFinish());
    }

    IEnumerator StandFinish(){
        // Wait until "Stand" is the current animation
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            yield return null;

        // Then wait until it's finished playing
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
    }

    void Update(){

        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        // Normalize and ignore any vertical component (we only care about the X-Z plane)
        camForward.y = 0; 
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // If the magnitude is too small, reset to default directions
        if (camForward.magnitude < 0.1f)
            camForward = Vector3.forward;

        if (camRight.magnitude < 0.1f)
            camRight = Vector3.right;

        // Get player movement input
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");

        // Determine if the camera is in top-down view (flat on the X-Z plane)
        bool isTopDownView = Mathf.Abs(camForward.y) < 0.1f;

        // Movement direction is relative to camera's orientation
        Vector3 moveDirection;

        if (isTopDownView)
            // In top-down view, the forward and right vectors will define the movement along the X and Z axes
            moveDirection = (camRight * moveInputX + camForward * moveInputZ).normalized;
        else
            moveDirection = (camRight * moveInputX + camForward * moveInputZ).normalized;

        // Apply movement
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // Rotate direction based on direction of movement
        if (moveDirection.magnitude > 0.1f){
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        // Handle Jump
        if (Input.GetButtonDown("Jump") && isOnGround){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }
}