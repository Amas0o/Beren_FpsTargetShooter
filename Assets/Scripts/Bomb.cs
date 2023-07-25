using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    private int prospectiveScore;
    // Start is called before the first frame update
    void Start()
    {
        prospectiveScore = -100;
        StartCoroutine("Death");
    }

    // Update is called once per frame
    public void AddDamage(float damage)
    {
        //Debug.Log("Took damage" +  damage); 
        health -= damage;
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
