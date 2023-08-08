using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WheelBonusLerp : MonoBehaviour
{
    float duration = Variables.wheelBonusLerpDuration;         // time within which the wheel bonus moves towards the player
    GameObject referenceObject;              // Player
    [SerializeField] TextMeshProUGUI text;   // the bonus
    Vector3 startPos;                        // Start position of the bonus visual i.e the position where wheel is destroyed
    int scoreBonus;                          // the bonus score the player will recieve when it shoots the wheel
    float timeElapsed = 0f;                  // used to keep track of time passed since object movement

    void Start()
    {
        // Basic initialization of variables
        referenceObject = GameObject.FindGameObjectWithTag("Player");  
        startPos = transform.position;
    }

    void Update() // calls the lerp function to move the player every frame
    {
        Lerping();
    }

    
    void Lerping()  // used to implement movement of gameObject towards the player
    {
        //Increments timeElasped with time passed in between function calls
        timeElapsed += Time.deltaTime;
        
        if (timeElapsed < duration)
        {
            // sets object position to next calculated step from the lerp function
            transform.position = Vector3.Lerp(startPos, referenceObject.transform.position, timeElapsed / duration);

        }
        else
        {
            //Destroys gameObject and gives the player the bonus
            transform.position = referenceObject.transform.position;
            GameManager.instance.AddToScore(scoreBonus);
            Destroy(gameObject);
        }
    }

    public void SetBonus(int score) // function used to set the score associated with the bonus
    {
        scoreBonus = score;
        text.SetText("+" + score.ToString());
    }


}
