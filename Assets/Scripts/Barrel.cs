using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float radius;
    [SerializeField] float damage;
    [SerializeField] int prospectiveScore;
    [SerializeField] GameObject scoreVisual;
    GameObject temp;
    float elaspedTime;
    HealthBarController healthBar;
    HealthBarController timeBar;
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
        //StartCoroutine("Death");
    }
    private void Start()
    {
        //StartCoroutine("Death");
        healthBar.UpdateHealth(health, maxHealth);
        explosion.transform.position = gameObject.transform.position;
        explosionSound = gameObject.GetComponent<AudioSource>();
        elaspedTime = 0;
    }
    public void UpdateInstance()
    {
        elaspedTime += Time.deltaTime;
        timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    public void AddDamage(float damage)
    {
        //Debug.Log("Took damage" +  damage); 
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)
        {
            //Debug.Log("Dead " + name);
            //GameManager.instance.AddToScore(prospectiveScore);
           
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

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
    public void Explode()
    {
        //Debug.Log("hello hello check" );
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
