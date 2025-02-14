using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.18f * 2;

    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask GroundMask;


    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastpositon = new Vector3(0f,0f,0f);

    private void Start() {
        controller = GetComponent<CharacterController>();

    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(lastpositon != gameObject.transform.position && isGrounded == true ){
            isMoving = true;
        }
        else {
            isMoving = false;
        }

        lastpositon = gameObject.transform.position;





        
    }


}
