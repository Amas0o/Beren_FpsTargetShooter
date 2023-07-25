using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float deathTime;

    private void Start()
    {
        StartCoroutine("Death");
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.gameObject.name);
        
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
        if ((collision.gameObject.tag == "Bullet") || (collision.gameObject.tag == "Pellet") )
        {
            return;
        }
        //Debug.Log("Collided with" + collision.gameObject.name);
        Destroy(gameObject);    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
