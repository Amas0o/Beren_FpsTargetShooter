using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Debug.Log("hit by " + collision.gameObject.name);
        int score;
        switch (gameObject.name)
        {
            case "200": score = 200; break;
            case "300": score = 300; break;
            case "400": score = 400; break;
            case "500": score = 500; break;
            default: score = 0; break;

        }
        var script = transform.parent.gameObject.GetComponent<WheelSpin>();
        script.StopRotation();
        transform.parent.gameObject.GetComponent<WheelSpin>().SetScore(score);
        transform.parent.gameObject.GetComponent<WheelSpin>().Disable();
        //GameManager.instance.AddToScore(score);
        //Debug.Log("collision on" + gameObject.name);
    }
        
    
}
 
