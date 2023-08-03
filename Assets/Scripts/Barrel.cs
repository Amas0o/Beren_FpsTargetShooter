using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] float health;              // health of the barrel
    [SerializeField] float deathTime;           // barrel lifetime
    [SerializeField] ParticleSystem explosion; 
    [SerializeField] float radius;              // radius within which surrounding objects will be damaged
    [SerializeField] float damage;              // damage to surrounding objects
    [SerializeField] int prospectiveScore;      // score that the player will gain upon barrel's destruction 
    [SerializeField] GameObject scoreVisual;    // floating visual of the score that the player will gain
    GameObject temp;                            // temporary variable for instantiating the scoreVisual
    float elaspedTime;                          // time elasped after spawing of the barrel
    HealthBarController healthBar;              // visual display of current health of the barrel
    HealthBarController timeBar;                // visual display of the reamaining barrel lifetime
    float maxHealth;                            
    AudioSource explosionSound;                 
    Target targetScript;                        
    Bomb bombScript;
    Upgrade upgradeScript;
    Barrel barrelScript;
    Collider[] colliders;

    private void Awake()
    {
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        
    }
    private void Start()
    {
        
        healthBar.UpdateHealth(health, maxHealth);                     // initializing health bar at max health
        explosion.transform.position = gameObject.transform.position;  // initializing the position of the explosion effect
        explosionSound = gameObject.GetComponent<AudioSource>();       
        elaspedTime = 0;
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

    public void AddDamage(float damage)  // gives damage to the barrel when shot
    {
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)  // when the health reaches zero, barrel is destroyed, it deals damage to the surrounding objects and adds the score
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



    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
    public void Explode() // function responsible for dealing damage to the surrounding objects upon barrel's destruction
    {
        
        colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        Debug.Log("hello hello" + colliders.Length);
        for(int i = 0; i < colliders.Length; i++)
        {
            targetScript = null;
            bombScript = null;
            upgradeScript = null;
            barrelScript = null;
            Debug.Log("in explosion" + colliders[i].gameObject.name);
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
