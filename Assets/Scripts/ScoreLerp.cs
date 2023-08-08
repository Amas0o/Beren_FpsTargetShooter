using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLerp : MonoBehaviour
{


    float duration = Variables.scoreLerpDuration;           // duration of the floating score effect
    float lerpDistance = Variables.scoreLerpDistance;       // distance the score(text) travels
    [SerializeField] TextMeshProUGUI text;                  // score 
    float timeElapsed = 0f;                                 // used to keep track of time passed since object movement
    void Update() // calls the lerp function to move the player every frame
    {
        Lerping();
    }
    
    void Lerping()   // function responsible for the score floating (vertical) effect 
    {
        Vector3 startPos = transform.position;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x, transform.position.y + lerpDistance, transform.position.z), timeElapsed / duration);
            timeElapsed += Time.deltaTime; //Increments timeElasped with time passed in between function calls
        }
        else
            Destroy(gameObject);
    }
    public void setText(int score) // setting the score text
    {
        if (score < 0)
        {
            text.SetText(score.ToString());
            text.color = Color.red;
        }
        else text.SetText("+" + score.ToString());
    }
}
