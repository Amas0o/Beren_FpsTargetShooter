using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static  GameManager instance = null;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject upgradePrefab;
    [SerializeField] GameObject barrelPrefab;
    [SerializeField] GameObject assault;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    Gun pistolScript;
    Gun assaultScript;
    Gun shotgunScript;
    [SerializeField] float frenzyTime;
    private int score = 0;
    float x = 0;
    float z = 0;
    float y = 0;
    bool targetSpawn = true;
    float targetSpawnTime;
    bool barrelSpawn = false;
    float barrelSpawnTime;
    float bombSpawnTime;
    bool bombSpawn = false;
    float upgradeSpawnTime;
    bool upgradeSpawn = false;
    public TextMeshProUGUI scoreDisplay;
    private void Start()
    {
        StartCoroutine("bombSpawnCd");
        StartCoroutine("upgradeSpawnCd");
        StartCoroutine("barrelSpawnCd");
        pistolScript = pistol.GetComponent<Gun>();
        assaultScript = assault.GetComponent<Gun>();
        shotgunScript = shotgun.GetComponent<Gun>();

        
    }
    private void Update()
    {
        if (targetSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 18f);
            y = Random.Range(-0.6f, 0.6f);
            Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            targetSpawn = false;
            StartCoroutine("spawnCd");
        }
        if(bombSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            Instantiate(bombPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            bombSpawn = false;
            StartCoroutine("bombSpawnCd");
        }
        if (upgradeSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            Instantiate(upgradePrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            upgradeSpawn = false;
            StartCoroutine("upgradeSpawnCd");
        }
        if (barrelSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            Instantiate(barrelPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            barrelSpawn = false;
            StartCoroutine("barrelSpawnCd");
        }
        scoreDisplay.SetText(score.ToString());

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

    public void Frenzy()
    {
        

        pistolScript.EnableFrenzy();
        assaultScript.EnableFrenzy();
        shotgunScript.EnableFrenzy();

        StartCoroutine("FrenzyTimer");
        Debug.Log("Frenzy enabled");
    }

    IEnumerator spawnCd()
    {
        //Debug.Log("me is work");
        targetSpawnTime = Random.Range(3, 6);
        yield return new WaitForSeconds(targetSpawnTime);
        targetSpawn = true;
    }

    IEnumerator bombSpawnCd()
    {
        //Debug.Log("me is work");
        bombSpawnTime = Random.Range(5, 10);
        yield return new WaitForSeconds(bombSpawnTime);
        bombSpawn = true;
    }

    IEnumerator upgradeSpawnCd()
    {
        //Debug.Log("me is work");
        upgradeSpawnTime = Random.Range(10, 15);
        yield return new WaitForSeconds(upgradeSpawnTime);
        upgradeSpawn = true;
    }

    IEnumerator barrelSpawnCd()
    {
        //Debug.Log("me is work");
        barrelSpawnTime = Random.Range(10, 15);
        yield return new WaitForSeconds(barrelSpawnTime);
        barrelSpawn = true;
    }
    IEnumerator FrenzyTimer()
    {
        yield return new WaitForSeconds(frenzyTime);
        
        pistolScript.DisableFrenzy();
        assaultScript.DisableFrenzy();
        shotgunScript.DisableFrenzy();
    }
}
