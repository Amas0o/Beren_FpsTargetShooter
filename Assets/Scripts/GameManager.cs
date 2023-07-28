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
    [SerializeField] GameObject wheelPrefab;
    [SerializeField] GameObject assault;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] ParticleSystem FrenzyParticles;
    List<GameObject> instanceList;
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
    bool wheelSpawn = true;
    public TextMeshProUGUI scoreDisplay;
    GameObject prefabInstance;
    private void Start()
    {
        StartCoroutine("bombSpawnCd");
        StartCoroutine("upgradeSpawnCd");
        StartCoroutine("barrelSpawnCd");
        StartCoroutine("wheelSpawnCd");
        pistolScript = pistol.GetComponent<Gun>();
        assaultScript = assault.GetComponent<Gun>();
        shotgunScript = shotgun.GetComponent<Gun>();
        instanceList = new List<GameObject>();
        
    }
    private void Update()
    {
        if (targetSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 18f);
            y = Random.Range(-0.6f, 0.6f);
            prefabInstance = Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            instanceList.Add(prefabInstance);
            targetSpawn = false;
            StartCoroutine("spawnCd");
        }
        if(bombSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(bombPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            bombSpawn = false;
            StartCoroutine("bombSpawnCd");
        }
        if (upgradeSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(upgradePrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            upgradeSpawn = false;
            StartCoroutine("upgradeSpawnCd");
        }
        if (barrelSpawn)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(barrelPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            barrelSpawn = false;
            StartCoroutine("barrelSpawnCd");
        }
        if (wheelSpawn)
        {
            prefabInstance = Instantiate(wheelPrefab, new Vector3(0.85f, 7.54f, -1.75f), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            wheelSpawn = false;
            StartCoroutine("wheelSpawnCd");
        }
        scoreDisplay.SetText(score.ToString());
        instanceList.RemoveAll(IsNull);
        //Debug.Log("List size is " + instanceList.Count);
        foreach (GameObject item in instanceList)
        {
            if (item.tag == "Target")
            {
                item.gameObject.GetComponent<Target>().UpdateInstance();

            }
            if (item.tag == "Bomb")
            {
                item.GetComponent<Bomb>().UpdateInstance();
          
            }
            if (item.tag == "Upgrade")
            {
                item.GetComponent<Upgrade>().UpdateInstance();

            }
            if (item.tag == "Barrel")
            {
                item.GetComponent<Barrel>().UpdateInstance();

            }
            if (item.tag == "WheelOfFortune")
            {
                item.GetComponent<WheelSpin>().UpdateInstance();

            }
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
        //Debug.Log("New Score is " + score);
    }

    public void Frenzy()
    {
        

        pistolScript.EnableFrenzy();
        assaultScript.EnableFrenzy();
        shotgunScript.EnableFrenzy();
        FrenzyParticles.Play();
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
    IEnumerator wheelSpawnCd()
    {
        yield return new WaitForSeconds(20f);
        wheelSpawn = true; 
    }
    IEnumerator FrenzyTimer()
    {
        yield return new WaitForSeconds(frenzyTime);
        
        pistolScript.DisableFrenzy();
        assaultScript.DisableFrenzy();
        shotgunScript.DisableFrenzy();
    }

    bool IsNull(GameObject test)
    {
        if(test == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
