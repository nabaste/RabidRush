using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public ZombiePool pool;
    public string poolTag;
    private List<float> _times;
    private float _counter;

    [NonSerialized] public Vector3 spawnPosition = new Vector3();

    
    private int _presentWave = 0;


    private void Awake()
    {
        _counter = 0;
        _times = pool.GetTimes(poolTag);
    }

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_counter < _times[_presentWave] || _times.Capacity >= _presentWave) return;
        _presentWave++;
        var zombie = pool.GetObject(poolTag);
        zombie.transform.position = spawnPosition;
    }
}