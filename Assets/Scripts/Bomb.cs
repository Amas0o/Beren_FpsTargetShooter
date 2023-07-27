using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    private int prospectiveScore;
    HealthBarController healthBar;
    HealthBarController timeBar;
    float elaspedTime;
    float maxHealth;
    // Start is called before the first frame update

    private void Awake()
    {
        prospectiveScore = -100;
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        timeBar = GetComponentsInChildren<HealthBarController>()[1];
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        elaspedTime = 0;
        //StartCoroutine("Death");
    }
    void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);
    }

    // Update is called once per frame
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
