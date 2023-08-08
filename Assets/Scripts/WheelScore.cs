using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour // Script used to determine score when WheelofFortune is shot
{
    private void OnCollisionEnter2D(Collision2D collision)   // the score is set according to the position where the wheel is shot
    {
        int score;
        
        // sets score according to the collidor that is activated
        switch (gameObject.name)
        {
            case "200": score = 200; break;
            case "300": score = 300; break;
            case "400": score = 400; break;
            case "500": score = 500; break;
            default: score = 0; break;

        }
        
        //Disables wheel rotation and bonus collidors also sets the bonus score
        transform.parent.gameObject.GetComponent<WheelSpin>().StopRotation();
        transform.parent.gameObject.GetComponent<WheelSpin>().SetScore(score);
        transform.parent.gameObject.GetComponent<WheelSpin>().Disable();
    }
        
    
}
 
