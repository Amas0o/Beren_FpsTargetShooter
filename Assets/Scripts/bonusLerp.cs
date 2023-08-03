using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class bonusLerp : MonoBehaviour       // implements bonus going to player functionality
{
    [SerializeField] float duration;         // specifies the duration in which the bonus reaches the player
    GameObject referenceObject;              // variable to store player gameObject
    Vector3 startPos;                        // variable to store this gameObjects start position
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
            GameManager.instance.Frenzy();
            Destroy(gameObject);
        }
    }

    
}
