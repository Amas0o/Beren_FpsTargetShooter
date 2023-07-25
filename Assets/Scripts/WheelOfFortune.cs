using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int score;
        switch (gameObject.name)
        {
            case "200": score = 200; break;
            case "300": score = 300; break;
            case "400": score = 400; break;
            case "500": score = 500; break;
            default: score = 0; break;

        }

        GameManager.instance.StopSpin();
        GameManager.instance.AddToScore(score);
        Debug.Log("collision on" + gameObject.name);
    }
}
