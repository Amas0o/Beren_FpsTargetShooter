using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLerp : MonoBehaviour
{


    [SerializeField] float duration;            // duration of the floating score effect
    [SerializeField] float lerpDistance;        // distance the score(text) travels
    [SerializeField] TextMeshProUGUI text;      // score 
    
    void Update()
    {
        Lerping();
    }


    float timeElapsed = 0f;
    void Lerping()   // function responsible for the score floating (vertical) effect 
    {
        Vector3 startPos = transform.position;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x, transform.position.y + lerpDistance, transform.position.z), timeElapsed / duration);
            timeElapsed += Time.deltaTime;
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
