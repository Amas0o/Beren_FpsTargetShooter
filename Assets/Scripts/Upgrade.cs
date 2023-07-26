using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    HealthBarController healthBar;
    float maxHealth;
    // Update is called once per frame
    private void Awake()
    {
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBarController>();
        if (healthBar == null)
        {
            Debug.Log("error");
        }
        StartCoroutine("Death");
    }
    private void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);
    }
    public void AddDamage(float damage)
    {
        //Debug.Log("Took damage" +  damage); 
        health -= damage;
        healthBar.UpdateHealth(health, maxHealth);
        if (health <= 0)
        {
            //Debug.Log("Dead " + name);
            GameManager.instance.Frenzy();
            Destroy(gameObject);
        }
    }

    IEnumerator Death() { 
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
