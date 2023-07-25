using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float deathTime;
    [SerializeField] ParticleSystem explosion;
    private void Start()
    {
        StartCoroutine("Death");
        explosion.transform.position = gameObject.transform.position;
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
            //GameManager.instance.AddToScore(prospectiveScore);
            explosion.Play();
            StartCoroutine("ExplosionDelay");
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
