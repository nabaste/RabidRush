using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public ZombiePool pool;
    public string poolTag;
    private List<float> _waveTimes;

    [NonSerialized] public Vector3 spawnPosition = new Vector3();
    
    public WaitForSeconds spacing;
    public int groupSize;

    private void Start()
    {
        _waveTimes = pool.GetTimes(poolTag);

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        foreach (var waveTime in _waveTimes)
        {
            yield return new WaitForSeconds(waveTime);
            StartCoroutine(SpawnZombies());
        }
    }

    private IEnumerator SpawnZombies()
    {
        for (int i = 0; i < groupSize; i++)
        {
            yield return spacing;
            var zombie = pool.GetObject(poolTag);
            zombie.transform.position = spawnPosition;
        }
    }
}