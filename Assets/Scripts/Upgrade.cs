using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float health;                    // health of the upgrade
    [SerializeField] float deathTime;                 // lifetime of the upgrdae
    HealthBarController healthBar;                    // visual display of current health of the upgrade
    HealthBarController timeBar;                      // visual display of the reamaining upgrade lifetime
    float elaspedTime;
    float maxHealth;
    [SerializeField] GameObject upgradeCollect;       // duplicate upgrade prefab for the upgrade collection effect
    GameObject temp;                                  // temporary variable for instantiating the duplicate upgrade


    private void Awake()
    {
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        elaspedTime = 0;
    }

    public void UpdateInstance()   // this function is the Update Instance for the master clock and is called in the Game Manager
    {
        elaspedTime += Time.deltaTime;
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);    // initializing health bar at max health
    }
    public void AddDamage(float damage)   // gives damage to the upgrade when shot
    {
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0) 
        {
            temp = Instantiate(upgradeCollect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

}
