using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour             // implements exploding barrel functionality
{
    [SerializeField] float health;              // health of the barrel
    [SerializeField] float deathTime;           // barrel lifetime
    [SerializeField] ParticleSystem explosion;  // particle system holding explosion particle effects
    [SerializeField] float radius;              // radius within which surrounding objects will be damaged
    [SerializeField] float damage;              // damage to surrounding objects
    [SerializeField] int prospectiveScore;      // score that the player will gain upon barrel's destruction 
    [SerializeField] GameObject scoreVisual;    // floating visual of the score that the player will gain
    GameObject temp;                            // temporary variable for instantiating the scoreVisual
    float elaspedTime;                          // time elasped after spawing of the barrel
    HealthBarController healthBar;              // visual display of current health of the barrel
    HealthBarController timeBar;                // visual display of the reamaining barrel lifetime
    float maxHealth;                            // maximum health of the target
    AudioSource explosionSound;                 // variable to hold explosion sound effect
    Target targetScript;                        // variable to hold scripts of targets affected by explosion
    Bomb bombScript;                            // variable to hold scripts of bombs affected by explosion
    Upgrade upgradeScript;                      // variable to hold scripts of frenzy bottles/upgrades affected by explosion
    Barrel barrelScript;                        // variable to hold scripts of other barrels affected by explosion
    Collider[] colliders;                       // list of colliders to hold objects within coillision radius
    //cache collioder

    private void Awake()
    {
        //Basic initialization of variables
        maxHealth = health;                                          
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        healthBar.UpdateHealth(health, maxHealth);                     // initializing health bar at max health
        explosion.transform.position = gameObject.transform.position;  // initializing the position of the explosion effect
        elaspedTime = 0;
        explosionSound = gameObject.GetComponent<AudioSource>();
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        
    }
    

    
    public void UpdateInstance()  // Handles all the time related update functionality, called by the master clock
    {
        //Increments elaspedTime with time passed in between function calls and updates timeBar bar to reflect that value
        elaspedTime += Time.deltaTime;         
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        
        //Destroys gameObject if lifetime of object has passed
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)  // gives damage to the barrel when shot
    {
        //Decrements health with damage taken and updates healthBar bar to reflect that value
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);

        if (health <= 0)  // when the health reaches zero, barrel is destroyed, it deals damage to the surrounding objects and adds to the score
        {
            gameObject.GetComponent<Collider>().enabled = false;
            Explode();
            explosion.Play();
            explosionSound.Play();
            GameManager.instance.AddToScore(prospectiveScore);
            temp = Instantiate(scoreVisual, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), gameObject.transform.rotation);
            temp.GetComponent<ScoreLerp>().setText(prospectiveScore);
            StartCoroutine("ExplosionDelay");
            
        }
    }



    IEnumerator ExplosionDelay() //creates a delay between explosion and deletion of gameObject to be able to view the particle effects
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
    public void Explode() // function responsible for dealing damage to the surrounding objects upon barrel's destruction
    {
        
        colliders = Physics.OverlapSphere(gameObject.transform.position, radius); // generates a radius around gameObject and returns all colliders within that radius
        
        for(int i = 0; i < colliders.Length; i++) //loops over all colliders within explosion radius adding explosion damage to their scripts
        {
            targetScript = null;
            bombScript = null;
            upgradeScript = null;
            barrelScript = null;
            //Debug.Log("in explosion" + colliders[i].gameObject.name);
            if (colliders[i].gameObject.tag == "Target")
            {
                targetScript = colliders[i].gameObject.GetComponent<Target>();
                if(targetScript != null)
                {
                    targetScript.AddDamage(damage);
                }
                else
                {
                    Debug.Log("error");
                }
                

            }
            else if (colliders[i].gameObject.tag == "Bomb")
            {
                bombScript = colliders[i].gameObject.GetComponent<Bomb>();
                if (bombScript != null)
                {
                    bombScript.AddDamage(damage);
                }
                else
                {
                    Debug.Log("error");
                }

            }
            else if (colliders[i].gameObject.tag == "Upgrade")
            {
                upgradeScript = colliders[i].gameObject.GetComponent<Upgrade>();
                if (upgradeScript != null)
                {
                    upgradeScript.AddDamage(damage);
                }
                else
                {
                    Debug.Log("error");
                }

            }
            else if (colliders[i].gameObject.tag == "Barrel")
            {
                barrelScript = colliders[i].gameObject.GetComponent<Barrel>();
                if (barrelScript != null)
                {
                    barrelScript.AddDamage(damage);
                }
                else
                {
                    Debug.Log("error");
                }

            }
        }
    }
}
