using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] float speed;
    bool rotation;
    [SerializeField] float deathTime;
    [SerializeField] float brakeSpeed;
    private void Start()
    {
        StartCoroutine("Death");
        rotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotation)
        {
            //Debug.Log("rotation in update is " + rotation);
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
        else
        {
            speed -= brakeSpeed;
            if(speed>0) { transform.Rotate(Vector3.forward, speed * Time.deltaTime); }
            else
            {
                //Debug.Log("no more ghoomrha" + speed);
                Destroy(gameObject);
            }
        }
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    public void StopRotation()
    {
        rotation = false;
    }
}
