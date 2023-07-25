using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Wheel : MonoBehaviour
{
    [SerializeField] float speed;
    bool rotation;
    [SerializeField] float deathTime;

    private void Start()
    {
       // StartCoroutine("Death");
       rotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("rotation in update is " + rotation);
        transform.Rotate(Vector3.forward, speed*Time.deltaTime);  
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
    
    public void StopRotation()
    {   
        Debug.Log("no more ghoomrha" + speed);
        Destroy(gameObject);
    }
}
