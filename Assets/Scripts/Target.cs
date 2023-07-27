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
    HealthBarController timeBar;
    float maxHealth;
    float elaspedTime;
    [SerializeField] GameObject scoreVisual;
    GameObject temp;
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
        //StartCoroutine("Death");
    }
    private void Start()
    {
        healthBar.UpdateHealth(health, maxHealth);
    }
    public void UpdateInstance()
    {
        elaspedTime+= Time.deltaTime;
        timeBar.UpdateHealth(deathTime-elaspedTime, deathTime);
        if(elaspedTime >= deathTime)
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
            GameManager.instance.AddToScore(prospectiveScore);
            temp = Instantiate(scoreVisual, new Vector3(gameObject.transform.position.x  ,gameObject.transform.position.y + 3 , gameObject.transform.position.z), Quaternion.identity);
            temp.GetComponent<ScoreLerp>().setText(prospectiveScore);
            Destroy(gameObject);
        }
    }

    IEnumerator Death() { 
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
