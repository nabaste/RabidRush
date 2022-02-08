using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    [Serializable]
    public class ObjectsPools
    {
        public string poolTag;
        public int size;
        public GameObject prefab;
        public Queue<GameObject> poolQueue;

        public List<float> spawnTimes;

        public ObjectsPools(string tag, int size, List<float> times,GameObject prefab)
        {
            this.poolTag = tag;
            this.size = size;
            this.prefab = prefab;
            this.spawnTimes = times;
        }
    }


    public static ZombiePool Instance;

    public List<ObjectsPools> pools;

    private Dictionary<string, ObjectsPools> _poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _poolDictionary = new Dictionary<string, ObjectsPools>();

        foreach (var item in pools)
        {
            item.poolQueue = new Queue<GameObject>();
            _poolDictionary.Add(item.poolTag, item);

            for (int i = 0; i < item.size; i++)
            {
                var poolObject = Instantiate(item.prefab);
                poolObject.SetActive(false);
                var iPoolableObject = poolObject.GetComponent<IPoolable>();
                iPoolableObject.PoolTag = item.poolTag;
                item.poolQueue.Enqueue(poolObject);
            }
        }
    }

    private void RecycleObject(string pooltag, GameObject objectToRecycle)
    {
        print("Reciclado");
        var currentPool = _poolDictionary[pooltag];
        currentPool.poolQueue.Enqueue(objectToRecycle);
    }

    public GameObject GetObject(string poolTag)
    {
        if (!_poolDictionary.ContainsKey(poolTag))
        {
            throw new Exception($"There is no pool tagged {poolTag}");
        }

        var currentPool = _poolDictionary[poolTag];


        if (currentPool.poolQueue.Count == 0)
        {
            var poolObject = Instantiate(currentPool.prefab);
            poolObject.SetActive(false);
            var iPoolableObject1 = poolObject.GetComponent<IPoolable>();
            iPoolableObject1.PoolTag = currentPool.poolTag;
            currentPool.poolQueue.Enqueue(poolObject);
        }

        var objectToReturn = currentPool.poolQueue.Dequeue();
        objectToReturn.SetActive(true);

        var iPoolableObject = objectToReturn.GetComponent<IPoolable>();
        iPoolableObject.OnEliminateObject += RecycleObject;
        return objectToReturn;
    }

    public List<float> GetTimes(string poolTag)
    {
        if (!_poolDictionary.ContainsKey(poolTag))
        {
            throw new Exception($"There is no pool tagged {poolTag}");
        }

        var currentPool = _poolDictionary[poolTag];
        return _poolDictionary[poolTag].spawnTimes;
    }
}