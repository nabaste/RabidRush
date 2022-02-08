using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public ZombiePool pool;

    public float spawnTime;
    private float _counter;
    public string poolTag;

    [NonSerialized] public Vector3 spawnPosition = new Vector3();

    public List<float> waveTimes = new List<float>();
    private int _presentWave = 0;


    private void Awake()
    {
        _counter = 0;
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter > waveTimes[_presentWave])
        {
            // _counter = 0;
            _presentWave++;

            var zombie = pool.GetObject(poolTag);
            zombie.transform.position = spawnPosition;
        }
    }
}