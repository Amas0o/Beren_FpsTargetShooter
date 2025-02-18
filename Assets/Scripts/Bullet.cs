using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour                 // handles functionality of gun bullets
{
    float deathTime = Variables.bulletDeathTime;    // bullet lifetime
    float damage = Variables.bulletDamage;          // damage that the bullet deals to targets


    private void OnEnable()
    {
        StartCoroutine("Death");       // starts coroutine to delete bullet after deathTime has passed
    }

    private void OnCollisionEnter(Collision collision)  // deals damage to the targets
    {
        // Checks tag of the target hit and gets the relevant script to call the addDamage function on that target
        if(collision.gameObject.tag == TagHolder.target)
        {
            collision.gameObject.GetComponent<Target>().AddDamage(damage);

        }
        if (collision.gameObject.tag == TagHolder.bomb)
        {
            collision.gameObject.GetComponent<Bomb>().AddDamage(damage);

        }
        if (collision.gameObject.tag == TagHolder.upgrade)
        {
            collision.gameObject.GetComponent<Upgrade>().AddDamage(damage);

        }
        if (collision.gameObject.tag == TagHolder.barrel)
        {
            collision.gameObject.GetComponent<Barrel>().AddDamage(damage);

        }
        if ((collision.gameObject.tag == TagHolder.bullet))
        {
            return;
        }

        //Destroy(gameObject);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    IEnumerator Death() // bullet is destroyed after a certain time
    {
        yield return new WaitForSeconds(deathTime);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Shoot(float shootForce)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * shootForce, ForceMode.Impulse);
    }
}
