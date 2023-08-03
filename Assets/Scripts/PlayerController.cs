using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 move_direction;                      // player's direction of movement
    [SerializeField] float speed;                // player's speed
    [SerializeField] float sprintSpeed;          // player's speed while sprinting
    CharacterController characterController;     // controller script
    float verticalSpeed;                           
    [SerializeField] float jumpForce;            // jump force of the player
    [SerializeField] float gravity;              
    float tempSpeed;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        move_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        Sprint();
        move_direction *= speed * Time.deltaTime;
        Gravity();
        characterController.Move(move_direction);
        
    }

    private void Gravity()
    {
        verticalSpeed -= gravity * Time.deltaTime;
        Jump();
        move_direction.y = verticalSpeed * Time.deltaTime;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            verticalSpeed = jumpForce;

    }
    private void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            tempSpeed = speed;
            speed = sprintSpeed;
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = tempSpeed;
        }
    }
}

