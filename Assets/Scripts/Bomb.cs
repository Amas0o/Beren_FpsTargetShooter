using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    private int prospectiveScore;
    HealthBarController healthBar;
    float maxHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        prospectiveScore = -100;
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        StartCoroutine("Death");
    }
    void Start()
    {
        
        
        healthBar.UpdateHealth(health, maxHealth);
    }

    // Update is called once per frame
    public void AddDamage(float damage)
    {
        //Debug.Log("Took damage" +  damage); 
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)
        {   
            
            Debug.Log("Bomb Exploded");
            GameManager.instance.AddToScore(prospectiveScore);
            Destroy(gameObject);
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    
}
