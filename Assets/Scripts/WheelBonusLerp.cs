using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class WheelBonusLerp : MonoBehaviour
{
    [SerializeField] float duration;         // time within which the wheel bonus moves towards the player
    GameObject referenceObject;              // Player
    [SerializeField] TextMeshProUGUI text;   // the bonus
    Vector3 startPos;                        // Start position of the bonus visual i.e the position where wheel is destroyed
    int scoreBonus;                          // the bonus score the player will recieve when it shoots the wheel


    
    void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("Player");  
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Lerping();
    }

    float timeElapsed = 0f;
    void Lerping()   // function  responsible for the floating bonus effect
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, referenceObject.transform.position, timeElapsed / duration);

        }
        else
        {
            transform.position = referenceObject.transform.position;
            GameManager.instance.AddToScore(scoreBonus);
            Destroy(gameObject);
        }
    }

    public void SetBonus(int score) 
    {
        scoreBonus = score;
        text.SetText("+" + score.ToString());
    }


}
