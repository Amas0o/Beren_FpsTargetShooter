using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class bonusLerp : MonoBehaviour
{
    [SerializeField] float duration;
    GameObject referenceObject;
    Vector3 startPos;
    bool bonus;
    int scoreBonus;
    // Start is called before the first frame update
    void Start()
    {
        referenceObject = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        //bonus = false;
    }

    // Update is called once per frame
    void Update()
    {
        Lerping();
    }

    float timeElapsed = 0f;
    void Lerping()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, referenceObject.transform.position, timeElapsed / duration);
            
        }
        else
        {
            Debug.Log("Bonus is " + bonus + " score is " + scoreBonus );
            transform.position = referenceObject.transform.position;
            if(!bonus) GameManager.instance.Frenzy();
            else GameManager.instance.AddToScore(scoreBonus);
            Destroy(gameObject);
        }
    }

    public void SetBonus(int score)
    {
        
        bonus = true;
        Debug.Log("Setting bonus to " + bonus);
        scoreBonus = score;
    }

}
