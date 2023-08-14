using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject TargetPrefab;
    [SerializeField] GameObject BombPrefab;
    [SerializeField] GameObject UpgradePrefab;
    [SerializeField] GameObject BarrelPrefab;
    [SerializeField] GameObject WheelOfFortunePrefab;

    public static ObjectPooler instance;
    public class Pool
    {
        string tag;
        GameObject prefab;
        int size;

        public Pool(string tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }

        public int getSize()
        {
            return this.size;
        }

        public GameObject getPrefab()
        {
            return this.prefab;
        }

        public string getTag()
        {
            return this.tag;
        }
    }

    


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        pools = new List<Pool>
        {
            new Pool(Variables.pool1Tag, BulletPrefab, Variables.pool1Size),
            new Pool(Variables.pool2Tag, TargetPrefab, Variables.pool2Size),
            new Pool(Variables.pool3Tag, BombPrefab, Variables.pool3Size),
            new Pool(Variables.pool4Tag, UpgradePrefab, Variables.pool4Size),
            new Pool(Variables.pool5Tag, BarrelPrefab, Variables.pool5Size),
            new Pool(Variables.pool6Tag, WheelOfFortunePrefab, Variables.pool6Size)
        };

    }

    List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    


   

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i  = 0; i < pool.getSize(); i++)
            {
                GameObject temp = Instantiate(pool.getPrefab());
                temp.SetActive(false);
                objectPool.Enqueue(temp);
            }

            poolDictionary.Add(pool.getTag(), objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject temp = poolDictionary[tag].Dequeue();

        temp.SetActive(true);
        temp.transform.position = position;
        temp.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(temp);

        return temp;
    }
}
