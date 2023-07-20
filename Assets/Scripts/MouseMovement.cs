using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] float sensitivity;
    Vector2 currentMouseLook;
    Vector2 lookAngles;
    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LockUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    private void LockUnlockCursor()
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

    private void LookAround()
    {

        currentMouseLook.x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        currentMouseLook.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRotation -= currentMouseLook.y;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);
        //Debug.Log("value  " + currentMouseLook.y);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.transform.Rotate(Vector3.up * currentMouseLook.x);
    }
}
