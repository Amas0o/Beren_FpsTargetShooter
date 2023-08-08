using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bomb : MonoBehaviour                               // implements bomb functionality
{
    float health = Variables.bombHealth;                        // health of the bomb
    float deathTime = Variables.bombDeathTime;                  // lifetime of the bomb
    int prospectiveScore = Variables.bombProspectiveScore;      // score that the player will gain upon bomb's destruction
    HealthBarController healthBar;                              // visual display of current health of the bomb
    HealthBarController timeBar;                                // visual display of the reamaining bomb lifetime
    float elaspedTime;                                          // time elasped after spawing of the bomb
    float maxHealth;                                            // variable to store maxiumum possible health of target
    [SerializeField] GameObject scoreVisual;                    // floating visual of the score that the player will gain
    GameObject temp;                                            // temporary variable for instantiating the scoreVisual
    

    private void OnEnable()
    {
        // Basic initialization of variables
        health = maxHealth;
        
        healthBar = GetComponentInChildren<HealthBarController>();
        healthBar.UpdateHealth(health, maxHealth);  // initializing health bar at max health

        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (healthBar == null)
        {
            Debug.Log("error");
        }

        elaspedTime = 0;
       
    }

    private void Awake()
    {
        maxHealth = health;
    }

    public void UpdateInstance()  // Handles all the time related update functionality, called by the master clock
    {
        //Increments elaspedTime with time passed in between function calls and updates timeBar bar to reflect that value
        elaspedTime += Time.deltaTime;
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);

        //Destroys gameObject if lifetime of object has passed
        if (elaspedTime >= deathTime)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void AddDamage(float damage)  // gives damage to the bomb when shot
    {
        //Decrements health with damage taken and updates healthBar bar to reflect that value
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);

        if (health <= 0)  // when the health reaches zero, bomb is destroyed and the negative score is added i.e the player loses points
        {   
            GameManager.instance.AddToScore(prospectiveScore);
            temp = Instantiate(scoreVisual, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), gameObject.transform.rotation);
            temp.GetComponent<ScoreLerp>().setText(prospectiveScore);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
   

    
}
