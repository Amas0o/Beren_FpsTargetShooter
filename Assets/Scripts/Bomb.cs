using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float health;              // health of the bomb
    [SerializeField] float deathTime;           // lifetime of the bomb
    [SerializeField] int prospectiveScore;      // score that the player will gain upon bomb's destruction
    HealthBarController healthBar;              // visual display of current health of the bomb
    HealthBarController timeBar;                // visual display of the reamaining bomb lifetime
    float elaspedTime;                          // time elasped after spawing of the bomb
    float maxHealth;
    [SerializeField] GameObject scoreVisual;    // floating visual of the score that the player will gain
    GameObject temp;                            // temporary variable for instantiating the scoreVisual
    

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
    void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);  // initializing health bar at max health
    }

    
    public void UpdateInstance()  // this function is the Update Instance for the master clock and is called in the Game Manager
    {
        elaspedTime += Time.deltaTime;
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)  // gives damage to the bomb when shot
    { 
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)  // when the health reaches zero, bomb is destroyed and the negative score is added i.e the player loses points
        {   
            GameManager.instance.AddToScore(prospectiveScore);
            temp = Instantiate(scoreVisual, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), gameObject.transform.rotation);
            temp.GetComponent<ScoreLerp>().setText(prospectiveScore);
            Destroy(gameObject);
        }
    }
   

    
}
