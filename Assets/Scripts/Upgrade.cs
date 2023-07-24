using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine("Death");
    }
    public void AddDamage(float damage)
    {
        //Debug.Log("Took damage" +  damage); 
        health -= damage;
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
