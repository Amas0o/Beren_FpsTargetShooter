using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour   // handles weapon switching functionality
{
    int selectedWeapon = 0;                   // index of current weapon selected
    [SerializeField] GameObject crosshair;    // hold UI element containing crosshair
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {

        int previousSelectedWeapon = selectedWeapon; // saves current index

        // Gets scrollwheel input and if scrowheel was moved changes the selected weapon index
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else selectedWeapon--;
        }

        // checks if previous index is not equal to current index and calls weapon select
        if (previousSelectedWeapon != selectedWeapon)
        {
            if (!crosshair.activeSelf)
            {
                // handling crosshair and laser switching in between weapon switching
                crosshair.SetActive(true);
                transform.GetChild(previousSelectedWeapon).GetComponent<Gun>().TurnLaserOff();
            }
            SelectWeapon();
            
        }

    }

    void SelectWeapon()  // changes the selected weapon according to the current selectedWeapon index
    {
        int i = 0;
        
        // sets active the weapon on selectedWeapon Index
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
}   
