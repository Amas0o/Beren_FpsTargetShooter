using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            var script = collision.gameObject.GetComponent<Target>();
            script.AddDamage(10);

        }
        Debug.Log("Collided with" + collision.gameObject.name);
        Destroy(gameObject);    
    }
}
