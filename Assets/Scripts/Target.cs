using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health;
    private int prospectiveScore;

    private void Start()
    {
        prospectiveScore = 100;
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
            Debug.Log("Dead " + name);
            GameManager.instance.AddToScore(prospectiveScore);
            Destroy(gameObject);
        }
    }
}
