using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static  GameManager instance = null;
    [SerializeField] GameObject targetPrefab;
    private int score = 0;
    float x = 0;
    float z = 0;
    float y = 0;
    bool spawn = true;
    float spawnTime;
    private void Update()
    {
        if (spawn)
        {
            x = Random.Range(-4.5f, 5.5f);
            z = Random.Range(-6f, 18f);
            y = Random.Range(-0.6f, 0.6f);
            Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            spawn = false;
            StartCoroutine("spawnCd");
        }
        
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddToScore(int addScore)
    {
        score += addScore;
        Debug.Log("New Score is " + score);
    }

    IEnumerator spawnCd()
    {
        //Debug.Log("me is work");
        spawnTime = Random.Range(3, 6);
        yield return new WaitForSeconds(spawnTime);
        spawn = true;
    }
}
