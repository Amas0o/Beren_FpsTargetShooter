using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] float speed;                // rotation speed
    bool rotation;                               // bolean variable for enabling and disabling rotation
    [SerializeField] float deathTime;            // wheel lifetime
    [SerializeField] float brakeSpeed;           // decrementation of the rotation speed when the wheel is shot
    [SerializeField] GameObject ScoreCollect;    // floating visual of the score that the player will gain
    bool isNegative = false;                     // checking for the negative speed i.e rotating the wheel in the opposite direction
    int prospectiveScore;                        // score that the player will gain upon when the wheel is shot
    float elaspedTime;                  
    GameObject temp;                             // temporary variable for instantiating the ScoreCollect
    private void Start()
    {
        rotation = true;
        elaspedTime = 0;
    }
    public void UpdateInstance()   // this function is the Update Instance for the master clock and is called in the Game Manager
    {
        elaspedTime += Time.deltaTime;
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
        if (rotation)
        {
            if (isNegative) transform.Rotate(Vector3.back, speed * Time.deltaTime);  // rotating wheel in the opposite direction
            else transform.Rotate(Vector3.forward, speed * Time.deltaTime);         
        }
        else
        {
            speed -= brakeSpeed;
            if(speed>0) { transform.Rotate(Vector3.forward, speed * Time.deltaTime); }
            else
            {
                temp = Instantiate(ScoreCollect, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<WheelBonusLerp>().SetBonus(prospectiveScore);
                Destroy(gameObject);
            }
        }
        
    }


    public void StopRotation()
    {
        rotation = false;
    }

    public void Disable()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject); 
        }
        
    }
    public void SetScore(int score)  // setting the score according to the position where the wheel is shot
    {
        prospectiveScore = score;
    }
    public void SetisNegative() 
    {
        isNegative = true;
    }

}
