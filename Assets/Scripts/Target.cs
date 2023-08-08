using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    float health = Variables.targetHealth;                          // health of the target
    int prospectiveScore = Variables.targetProspectiveScore;        // score that the player will gain upon target's destruction
    float deathTime = Variables.targetDeathTime;                    // lifetime of the target
    HealthBarController healthBar;                                  // visual display of current health of the target
    HealthBarController timeBar;                                    // visual display of the reamaining target lifetime
    float maxHealth;                                                // variable to store maxiumum possible health of target
    float elaspedTime;                                              // time elasped after spawing of the target
    [SerializeField] GameObject scoreVisual;                        // floating visual of the score that the player will gain
    GameObject temp;                                                // temporary variable for instantiating the scoreVisual
    private void Awake()
    {
        // Basic initialization of variables
        maxHealth = health;
        elaspedTime = 0;
        
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        healthBar.UpdateHealth(health, maxHealth);   // initializing health bar at max health

        if (healthBar == null)
        {
            Debug.Log("error");
        }
        
    }
    public void UpdateInstance()    // Handles all the time related update functionality, called by the master clock
    {
        //Increments elaspedTime with time passed in between function calls and updates timeBar bar to reflect that value
        elaspedTime += Time.deltaTime;
        timeBar.UpdateHealth(deathTime-elaspedTime, deathTime);

        //Destroys gameObject if lifetime of object has passed
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)  // gives damage to the target when shot
    {
        //Decrements health with damage taken and updates healthBar bar to reflect that value
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);

        if (health <= 0)    // when the health reaches zero, target is destroyed and the score is added
        {
            GameManager.instance.AddToScore(prospectiveScore);
            temp = Instantiate(scoreVisual, new Vector3(gameObject.transform.position.x  ,gameObject.transform.position.y + 3 , gameObject.transform.position.z), Quaternion.identity);
            temp.GetComponent<ScoreLerp>().setText(prospectiveScore);
            Destroy(gameObject);
        }
    }

}
