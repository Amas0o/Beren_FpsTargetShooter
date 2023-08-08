using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    #endregion 
   

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i  = 0; i < pool.size; i++)
            {
                GameObject temp = Instantiate(pool.prefab);
                temp.SetActive(false);
                objectPool.Enqueue(temp);
            }

            poolDictionary.Add(pool.tag, objectPool);
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
