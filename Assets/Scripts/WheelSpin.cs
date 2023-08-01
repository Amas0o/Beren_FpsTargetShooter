using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class WheelSpin : MonoBehaviour
{
    [SerializeField] float speed;
    bool rotation;
    [SerializeField] float deathTime;
    [SerializeField] float brakeSpeed;
    [SerializeField] GameObject ScoreCollect;//WheelOfFortuneCollect;
    bool isNegative = false;
    int prospectiveScore;
    //HealthBarController timeBar;
    float elaspedTime;
    GameObject temp;
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
            if (isNegative) transform.Rotate(Vector3.back, speed * Time.deltaTime);
            else transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
        else
        {
            speed -= brakeSpeed;
            if(speed>0) { transform.Rotate(Vector3.forward, speed * Time.deltaTime); }
            else
            {
                //Debug.Log("no more ghoomrha" + speed);
                //Instantiate upgrade
                temp = Instantiate(ScoreCollect, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<WheelBonusLerp>().SetBonus(prospectiveScore);
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

    public void Disable()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject); 
        }
        
    }
    public void SetScore(int score)
    {
        prospectiveScore = score;
    }
    public void SetisNegative()
    {
        isNegative = true;
    }

}
