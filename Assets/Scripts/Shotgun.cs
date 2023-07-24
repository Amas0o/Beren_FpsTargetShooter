using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.EditorTools;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int pelletCount;
    public float spreadAngle;
    public GameObject pellet;
    public Transform BarrelExit;
    List<Quaternion> pellets;
    public float pelletFireVel;

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            fire();
    }

    void fire()
    {
        int i = 0;
        foreach (Quaternion quat in pellets.ToArray())
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pellet, BarrelExit.position, BarrelExit.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * pelletFireVel);
            i++;
        }


    }
}
