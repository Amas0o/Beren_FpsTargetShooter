using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] float speed;
    bool rotation;
    [SerializeField] float deathTime;
    [SerializeField] float brakeSpeed;
    //HealthBarController timeBar;
    float elaspedTime;
    private void Start()
    {
        //StartCoroutine("Death");
        //timeBar = GetComponentInChildren<HealthBarController>();
        rotation = true;
        elaspedTime = 0;
    }

    // Update is called once per frame
    public void UpdateInstance()
    {
        elaspedTime += Time.deltaTime;
        //timeBar.UpdateHealth(deathTime - elaspedTime, deathTime);
        if (elaspedTime >= deathTime)
        {
            Destroy(gameObject);
        }
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
                //Instantiate upgrade
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
