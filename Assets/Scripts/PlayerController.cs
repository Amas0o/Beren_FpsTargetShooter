using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour  // handles player movement
{

    Vector3 move_direction;                             // player's direction of movement
    float speed = Variables.playerSpeed;                // player's speed
    float sprintSpeed = Variables.playerSprintSpeed;    // player's speed while sprinting
    float jumpForce = Variables.playerJumpForce;        // jump force of the player
    float gravity = Variables.playerGravity;            // contains gravity force applied to player

    CharacterController characterController;     // controller script
    float verticalSpeed;                         // variable for current vertical speed
    float tempSpeed;                             // temporary variable for calculations
    
    void Start()
    {
        //Initializes with CharacterController
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Calculates direction to move in according to key pressed
        move_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move_direction = transform.TransformDirection(move_direction);
        
        //Calls sprint function to check if sprint key pressed and adds speed to movement
        Sprint();
        move_direction *= speed * Time.deltaTime;
        
        //Calls gravity to activate gravitational force on player
        Gravity();

        // Finally moves the player according to the calculated direction
        characterController.Move(move_direction);
        
    }

    private void Gravity() // implements gravitational force (decrements vertical speed by gravity)
    {
        verticalSpeed -= gravity * Time.deltaTime;
        Jump();
        move_direction.y = verticalSpeed * Time.deltaTime;
    }
    void Jump()  // handles jump functionality on keypress by adding vertical speed
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
            verticalSpeed = jumpForce;

    }
    private void Sprint() // toggles sprint on key press
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

