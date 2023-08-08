using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour  // handles target spawning, score, masterclock, upgrade functionality in the game
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

    List<GameObject> instanceList;                        // List of all present targets used to call update on them (masterclock)

    Gun pistolScript;                                     // used to hold pistol gun script
    Gun assaultScript;                                    // used to hold assault rifle gun script 
    Gun shotgunScript;                                    // used to hold shotgun gun script

    float frenzyTime = Variables.frenzyTime;              // duration of frenzy upgrade

    private int score = 0;                                // current score 
    float x = 0;                                          // used to hold temporary cordinates
    float z = 0;                                          // used to hold temporary cordinates
    float y = 0;                                          // used to hold temporary cordinates

    bool targetSpawn = true;                              // boolean variable to check if the target should be spawned
    bool barrelSpawn = false;                             // boolean variable to check if the barrel should be spawned
    bool bombSpawn = false;                               // boolean variable to check if the bomb should be spawned
    bool upgradeSpawn = false;                            // boolean variable to check if the upgrade(bottle) should be spawned
    bool wheelSpawn = true;                               // boolean variable to check if the wheel of fortune should be spawned
    
    float targetSpawnTime;                                // time after which new target is spawned
    float barrelSpawnTime;                                // time after which new barrel is spawned
    float bombSpawnTime;                                  // time after which new bomb is spawned
    float upgradeSpawnTime;                               // time after which new upgrade(bottle) is spawned
    
    [SerializeField] TextMeshProUGUI scoreDisplay;        // used to hold UI element to show score
    GameObject prefabInstance;                            // temporary gameObject

    ObjectPooler pooler;
    private void Start()
    {
        // Starts all spawner timers
        StartCoroutine("bombSpawnCd");
        StartCoroutine("upgradeSpawnCd");
        StartCoroutine("barrelSpawnCd");
        StartCoroutine("wheelSpawnCd");

        // Initializes scripts variables with the relevant scripts
        pistolScript = pistol.GetComponent<Gun>();
        assaultScript = assault.GetComponent<Gun>();
        shotgunScript = shotgun.GetComponent<Gun>();

        // Initializes instance list
        instanceList = new List<GameObject>();
        pooler = ObjectPooler.instance;
    }
    private void Update()
    {
        if (targetSpawn)   // handles spawining of the target if targetSpawn is true
        {
            // Assigns random cordinates in the specified range
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 18f);
            y = Random.Range(-0.6f, 0.6f);

            //prefabInstance = Instantiate(targetPrefab, new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            prefabInstance = pooler.SpawnFromPool("Target", new Vector3(x, y, z), Quaternion.Euler(90, 0, 0));
            instanceList.Add(prefabInstance);   // adds newly created target into the instance List

            targetSpawn = false;
            StartCoroutine("spawnCd");
        }

        if(bombSpawn)     // handles spawining of the bomb if bombSpawn is true
        {
            // Assigns random cordinates in the specified range
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);

            //prefabInstance = Instantiate(bombPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            prefabInstance = pooler.SpawnFromPool("Bomb", new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);   // adds newly created target into the instance List

            bombSpawn = false;
            StartCoroutine("bombSpawnCd");
        }

        if (upgradeSpawn)   // handles spawining of the upgrade if upgradeSpawn is true
        {
            // Assigns random cordinates in the specified range
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);

            //prefabInstance = Instantiate(upgradePrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            prefabInstance = pooler.SpawnFromPool("Upgrade", new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);  // adds newly created target into the instance List

            upgradeSpawn = false;
            StartCoroutine("upgradeSpawnCd");
        }

        if (barrelSpawn)   // handles spawining of the barrel if barrelSpawn is true
        {
            // Assigns random cordinates in the specified range
            x = Random.Range(-3.85f, 4.85f);
            z = Random.Range(-6f, 6f);
            y = Random.Range(0f, 1.7f);
            
            //prefabInstance = Instantiate(barrelPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            prefabInstance = pooler.SpawnFromPool("Barrel", new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);  // adds newly created target into the instance List

            barrelSpawn = false;
            StartCoroutine("barrelSpawnCd");
        }
        if (wheelSpawn)   // handles spawining of the wheel of fortune if wheelSpawn is true
        {
            // Assigns random cordinates in the specified range (here x and z both are used to specify the z cordinate)
            x = Random.Range(-6f, 6f);
            z = Random.Range(-6f, 6f);

            //prefabInstance = Instantiate(wheelPrefab, new Vector3(3f, 7.54f, x), Quaternion.Euler(0, 0, 0));
            prefabInstance = pooler.SpawnFromPool("WheelOfFortune", new Vector3(3f, 7.54f, x), Quaternion.Euler(0, 0, 0));
            instanceList.Add(prefabInstance);  // adds newly created target into the instance List

            //prefabInstance = Instantiate(wheelPrefab, new Vector3(-2.64f, 7.54f, z), Quaternion.Euler(0, 0, 0));
            prefabInstance = pooler.SpawnFromPool("WheelOfFortune", new Vector3(-2.64f, 7.54f, z), Quaternion.Euler(0, 0, 0));
            prefabInstance.GetComponent<WheelSpin>().SetisNegative(); // negates the speed to make wheel spin in other direction
            instanceList.Add(prefabInstance); // adds newly created target into the instance List

            wheelSpawn = false;
            StartCoroutine("wheelSpawnCd");
        }

        scoreDisplay.SetText(score.ToString());  // updates scoreDisplay with the current score
        instanceList.RemoveAll(IsNull);          // removes all destroyed/Null gameObjects from instance list
        

        foreach (GameObject item in instanceList)  // calling update instances of each target in the list for the master clock
        {
            if (item.tag == TagHolder.target)
            {
                item.gameObject.GetComponent<Target>().UpdateInstance();

            }
            if (item.tag == TagHolder.bomb)
            {
                item.GetComponent<Bomb>().UpdateInstance();
          
            }
            if (item.tag == TagHolder.upgrade)
            {
                item.GetComponent<Upgrade>().UpdateInstance();

            }
            if (item.tag == TagHolder.barrel)
            {
                item.GetComponent<Barrel>().UpdateInstance();

            }
            if (item.tag == TagHolder.wheelOfFortune)
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
        bombSpawnTime = Random.Range(5, 10);
        yield return new WaitForSeconds(bombSpawnTime);
        bombSpawn = true;
    }

    IEnumerator upgradeSpawnCd()  // spawing upgrade(bottle) at random times
    {
        upgradeSpawnTime = Random.Range(10, 15);
        yield return new WaitForSeconds(upgradeSpawnTime);
        upgradeSpawn = true;
    }

    IEnumerator barrelSpawnCd()    // spawing barrel at random times
    {
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

    bool IsNull(GameObject test)   // test for gameObject equals to Null
    {
        if(test.activeInHierarchy == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
