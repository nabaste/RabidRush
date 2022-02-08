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
        public List<float> spawnTimes;

        public Queue<GameObject> poolQueue;

        public ObjectsPools(string tag, int size, List<float> times, GameObject prefab)
        {
            this.poolTag = tag;
            this.size = size;
            this.prefab = prefab;
            this.spawnTimes = times;
        }
    }

    #region Singleton

    public static ZombiePool Instance;

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
    }

    #endregion


    public List<ObjectsPools> pools;
    [SerializeField] private Transform container;
    private Dictionary<string, ObjectsPools> _poolDictionary;

    public void CreatePoolObjects()
    {
        _poolDictionary = new Dictionary<string, ObjectsPools>();

        foreach (var item in pools)
        {
            item.poolQueue = new Queue<GameObject>();
            _poolDictionary.Add(item.poolTag, item);

            for (int i = 0; i < item.size; i++)
            {
                var poolObject = Instantiate(item.prefab, container, true);
                poolObject.SetActive(false);
                var iPoolableObject = poolObject.GetComponent<IPoolable>();
                iPoolableObject.PoolTag = item.poolTag;
                item.poolQueue.Enqueue(poolObject);
            }
        }
    }

    private void RecycleObject(string pooltag, GameObject objectToRecycle)
    {
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
            var poolObject = Instantiate(currentPool.prefab, container, true);
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