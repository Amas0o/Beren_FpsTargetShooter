using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    HealthBarController healthBar;
    HealthBarController timeBar;
    float elaspedTime;
    float maxHealth;
    [SerializeField] GameObject upgradeCollect;
    GameObject temp;
    // Update is called once per frame
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
        //StartCoroutine("Death");
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
            temp = Instantiate(upgradeCollect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    IEnumerator Death() { 
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
