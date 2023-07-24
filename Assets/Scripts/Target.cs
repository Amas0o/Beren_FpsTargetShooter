using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health;
    private int prospectiveScore;
    [SerializeField] float deathTime;
    private void Start()
    {
        prospectiveScore = 100;
        StartCoroutine("Death");
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
