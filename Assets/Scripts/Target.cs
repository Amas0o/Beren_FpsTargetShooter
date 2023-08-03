using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health;                // health of the target
    private int prospectiveScore;                 // score that the player will gain upon target's destruction
    [SerializeField] float deathTime;             // lifetime of the target
    HealthBarController healthBar;                // visual display of current health of the target
    HealthBarController timeBar;                  // visual display of the reamaining target lifetime
    float maxHealth;
    float elaspedTime;
    [SerializeField] GameObject scoreVisual;      // floating visual of the score that the player will gain
    GameObject temp;                              // temporary variable for instantiating the scoreVisual
    private void Awake()
    {
        prospectiveScore = 100;
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        elaspedTime = 0;
    }
    private void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);   // initializing health bar at max health
    }
    public void UpdateInstance()    // this function is the Update Instance for the master clock and is called in the Game Manager
    {
        elaspedTime+= Time.deltaTime;
        timeBar.UpdateHealth(deathTime-elaspedTime, deathTime);
        if(elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)  // gives damage to the target when shot
    {
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
