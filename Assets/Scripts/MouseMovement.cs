using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    float sensitivity = Variables.mouseSensitivity;    // mouse sensitivity
    Vector2 currentMouseLook;              // current position
    
    float xRotation = 0f;                 // variable to store current x rotation
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Starts cursor in locked state
        Cursor.visible = false;                   // Makes cursor invisible
    }

    // Update is called once per frame
    void Update()
    {
        LockUnlockCursor();             // Calls the function to toggle cursor lock
        
        if (Cursor.lockState == CursorLockMode.Locked) // if cursor is locked enables lookAroun functionality
        {
            LookAround();
        }
    }

    private void LockUnlockCursor()    // Function to toggle cursor lock based on keypress
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LookAround()       //Function to enable lookAround functionality
    {
        // Gets current mouse axis and stores them in relevant variables (scaled by sensitivity)
        currentMouseLook.x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentMouseLook.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // decrements xRotation by mouse Y axis
        xRotation -= currentMouseLook.y;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);    // maximum vision

        // sets local and parent rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.transform.Rotate(Vector3.up * currentMouseLook.x);
    }
}
