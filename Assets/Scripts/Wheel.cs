using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Spin : MonoBehaviour
{
    [SerializeField] float speed;
    public bool rotation = true;
    [SerializeField] float deathTime;

    private void Start()
    {
       // StartCoroutine("Death");
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation)
        {
            transform.Rotate(Vector3.forward, speed);
        }
        else
        {
            speed -= 0.025f;
            if (speed > 0) transform.Rotate(Vector3.forward, speed);
            else StartCoroutine("DeathAfterCollision");
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
    IEnumerator DeathAfterCollision()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
