using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health;
    private int prospectiveScore;
    [SerializeField] float deathTime;
    HealthBarController healthBar;
    float maxHealth;
    private void Awake()
    {
        prospectiveScore = 100;
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        if(healthBar == null)
        {
            Debug.Log("error");
        }
        //StartCoroutine("Death");
    }
    private void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddDamage(10f);
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
            GameManager.instance.AddToScore(prospectiveScore);
            Destroy(gameObject);
        }
    }

    IEnumerator Death() { 
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
