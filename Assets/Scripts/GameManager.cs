using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static  GameManager instance = null;
    [SerializeField] GameObject targetPrefab;             // Prefab of the target
    [SerializeField] GameObject bombPrefab;               // Prefab of the bomb
    [SerializeField] GameObject upgradePrefab;            // Prefab of the upgrade(bottle)
    [SerializeField] GameObject barrelPrefab;             // Prefab of the barrel
    [SerializeField] GameObject wheelPrefab;              // Prefab of the wheel of fortune
    [SerializeField] GameObject assault;                  // Assault rifle
    [SerializeField] GameObject pistol;                   // Pistol
    [SerializeField] GameObject shotgun;                  // Shotgun
    [SerializeField] ParticleSystem FrenzyParticles;      // visual effect when the frenzy is enabled
    List<GameObject> instanceList;                        // List of all present targets for the master clock
    Gun pistolScript;                                       
    Gun assaultScript;
    Gun shotgunScript;
    [SerializeField] float frenzyTime;                    // duration of frenzy after the destruction of the bottle 
    private int score = 0;                                // current score 
    float x = 0;
    float z = 0;
    float y = 0;
    bool targetSpawn = true;                              // boolean variable to check if the target should be spawned
    float targetSpawnTime;                                // time after which new target is spawned
    bool barrelSpawn = false;                             // boolean variable to check if the barrel should be spawned
    float barrelSpawnTime;                                // time after which new barrel is spawned
    float bombSpawnTime;                                  // time after which new bomb is spawned
    bool bombSpawn = false;                               // boolean variable to check if the bomb should be spawned
    float upgradeSpawnTime;                               // time after which new upgrade(bottle) is spawned
    bool upgradeSpawn = false;                            // boolean variable to check if the upgrade(bottle) should be spawned
    bool wheelSpawn = true;                               // boolean variable to check if the wheel of fortune should be spawned
    public TextMeshProUGUI scoreDisplay;                  // display of the current score
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
        if (targetSpawn)   // random spawining of the target
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 18f);
            y = Random.Range(-0.6f, 0.6f);
            prefabInstance = Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            instanceList.Add(prefabInstance);
            targetSpawn = false;
            StartCoroutine("spawnCd");
        }
        if(bombSpawn)     // random spawining of the bomb
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(bombPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            bombSpawn = false;
            StartCoroutine("bombSpawnCd");
        }
        if (upgradeSpawn)   // random spawining of the upgrade(bottle)
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(upgradePrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            upgradeSpawn = false;
            StartCoroutine("upgradeSpawnCd");
        }
        if (barrelSpawn)   // random spawining of the barrel
        {
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            prefabInstance = Instantiate(barrelPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            barrelSpawn = false;
            StartCoroutine("barrelSpawnCd");
        }
        if (wheelSpawn)   // random spawining of the wheels of fortune
        {
            x = Random.Range(-6f, 6f);
            z = Random.Range(-6f, 6f);
            prefabInstance = Instantiate(wheelPrefab, new Vector3(3f, 7.54f, x), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);
            prefabInstance = Instantiate(wheelPrefab, new Vector3(-2.64f, 7.54f, z), Quaternion.Euler(0, 0, 0));
            prefabInstance.GetComponent<WheelSpin>().SetisNegative();
            instanceList.Add(prefabInstance);
            wheelSpawn = false;
            StartCoroutine("wheelSpawnCd");
        }
        scoreDisplay.SetText(score.ToString());  // score display
        instanceList.RemoveAll(IsNull);
        

        foreach (GameObject item in instanceList)  // calling update instances of each target in the list for the master clock
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
    public void AddToScore(int addScore)  // score addition
    {
        score += addScore;
    }

    public void Frenzy()  // enabling frenzy for a certain time
    {
        pistolScript.EnableFrenzy();
        assaultScript.EnableFrenzy();
        shotgunScript.EnableFrenzy();
        FrenzyParticles.Play();
        StartCoroutine("FrenzyTimer");
    }

    

    IEnumerator spawnCd()  // spawing target at random times
    {
        targetSpawnTime = Random.Range(3, 6);
        yield return new WaitForSeconds(targetSpawnTime);
        targetSpawn = true;
    }

    IEnumerator bombSpawnCd()  // spawing bomb at random times
    {
        //Debug.Log("me is work");
        bombSpawnTime = Random.Range(5, 10);
        yield return new WaitForSeconds(bombSpawnTime);
        bombSpawn = true;
    }

    IEnumerator upgradeSpawnCd()  // spawing upgrade(bottle) at random times
    {
        //Debug.Log("me is work");
        upgradeSpawnTime = Random.Range(10, 15);
        yield return new WaitForSeconds(upgradeSpawnTime);
        upgradeSpawn = true;
    }

    IEnumerator barrelSpawnCd()    // spawing barrel at random times
    {
        //Debug.Log("me is work");
        barrelSpawnTime = Random.Range(10, 15);
        yield return new WaitForSeconds(barrelSpawnTime);
        barrelSpawn = true;
    }
    IEnumerator wheelSpawnCd()     // spawing wheels of fortune at random times
    {
        yield return new WaitForSeconds(20f);
        wheelSpawn = true; 
    }
    IEnumerator FrenzyTimer()      // disabling of frenzy after a certain time
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
