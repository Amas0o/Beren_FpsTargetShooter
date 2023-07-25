using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision on" + gameObject.name);
    }
}
