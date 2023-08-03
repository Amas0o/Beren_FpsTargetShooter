using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float deathTime;  // bullet lifetime

    private void Start()
    {
        StartCoroutine("Death"); 
    }

    private void OnCollisionEnter(Collision collision)  // deals damage to the targets
    {
        
        if(collision.gameObject.tag == "Target" )
        {
            var script = collision.gameObject.GetComponent<Target>();
            script.AddDamage(10);

        }
        if (collision.gameObject.tag == "Bomb")
        {
            var script = collision.gameObject.GetComponent<Bomb>();
            script.AddDamage(10);

        }
        if (collision.gameObject.tag == "Upgrade")
        {
            var script = collision.gameObject.GetComponent<Upgrade>();
            script.AddDamage(10);

        }
        if (collision.gameObject.tag == "Barrel")
        {
            var script = collision.gameObject.GetComponent<Barrel>();
            script.AddDamage(10);

        }
        if ((collision.gameObject.tag == "Bullet"))
        {
            return;
        }
        
        Destroy(gameObject);    
    }

    IEnumerator Death() // bullet is destroyed after a certain time
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
