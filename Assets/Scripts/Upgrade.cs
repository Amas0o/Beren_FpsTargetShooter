using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    float health = Variables.upgradeHealth;           // health of the upgrade
    float deathTime = Variables.upgradeDeathTime;     // lifetime of the upgrdae
    HealthBarController healthBar;                    // visual display of current health of the upgrade
    HealthBarController timeBar;                      // visual display of the reamaining upgrade lifetime
    float elaspedTime;                                // time elasped after spawing of the upgrade
    float maxHealth;                                  // variable to store maxiumum possible health of target
    [SerializeField] GameObject upgradeCollect;       // duplicate upgrade prefab for the upgrade collection effect
    GameObject temp;                                  // temporary variable for instantiating the duplicate upgrade


    private void OnEnable()
    {
        // Basic initialization of variables
        health = maxHealth;
        elaspedTime = 0;

        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        healthBar.UpdateHealth(health, maxHealth);    // initializing health bar at max health
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        
    }

    private void Awake()
    {
        maxHealth = health;
    }

    public void UpdateInstance()   // Handles all the time related update functionality, called by the master clock
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
    public void AddDamage(float damage)   // gives damage to the upgrade when shot
    {
        //Decrements health with damage taken and updates healthBar bar to reflect that value
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);

        // when the health reaches zero, target is destroyed and the score is added
        if (health <= 0) 
        {
            temp = Instantiate(upgradeCollect, gameObject.transform.position, gameObject.transform.rotation);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

}
