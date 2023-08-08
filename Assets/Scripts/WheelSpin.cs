using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class WheelSpin : MonoBehaviour           //Script to handle WheelOfFortune functionality
{
    [SerializeField] float speed;                // rotation speed
    [SerializeField] float deathTime;            // wheel lifetime
    [SerializeField] float brakeSpeed;           // decrementation of the rotation speed when the wheel is shot
    [SerializeField] GameObject ScoreCollect;    // floating visual of the score that the player will gain
    
    bool isNegative = false;                     // checking for the negative speed i.e rotating the wheel in the opposite direction
    bool rotation;                               // bolean variable for enabling and disabling rotation
    
    int prospectiveScore;                        // score that the player will gain upon when the wheel is shot
    float elaspedTime;                           // holds time elasped 
    float maxSpeed;

    GameObject temp;                             // temporary variable for instantiating the ScoreCollect
    private void OnEnable()
    {
        //Basic initialization of variables
        rotation = true;
        elaspedTime = 0;
        speed = maxSpeed;
        Enable();
    }

    private void Awake()
    {
        maxSpeed = speed;
    }
    public void UpdateInstance()   // Handles all the time related update functionality, called by the master clock
    {
        //Increments elaspedTime with time passed in between function calls
        elaspedTime += Time.deltaTime;

        //Destroys gameObject if lifetime of object has passed
        if (elaspedTime >= deathTime)
        {
            gameObject.SetActive(false);
        }

        //Rotates gameobject if rotation is set to true
        if (rotation)
        {
            if (isNegative) transform.Rotate(Vector3.back, speed * Time.deltaTime);  // rotating wheel in the opposite direction
            else transform.Rotate(Vector3.forward, speed * Time.deltaTime);         
        }
        else
        {
            //Applies brakeforce to stop wheel rotation if current speed is greater than 0
            speed -= brakeSpeed;
            if(speed>0) { transform.Rotate(Vector3.forward, speed * Time.deltaTime); }
            
            else
            {
                //Spawns the bonus prefab and destroys gameObject
                temp = Instantiate(ScoreCollect, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<WheelBonusLerp>().SetBonus(prospectiveScore);
                gameObject.SetActive(false);
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
            t.gameObject.SetActive(false);
        }
        
    }

    void Enable()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
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
