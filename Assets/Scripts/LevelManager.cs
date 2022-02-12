using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RabidRush.ScriptableObjects;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelList levelList;
    public LevelData levelData;
    [SerializeField] public PlayerData pd;
    public int money;

    public int livesLeft;

    [SerializeField] public LootManager lm;
    private Action loot1;
    private Action loot2;

    #region Singleton

    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Level Manager is Null!");
            }

            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        _instance = this;
        levelData = levelList.levels[SceneManager.GetActiveScene().buildIndex];
    }

    private void Start()
    {
        money = levelData.StartingKarts;
        livesLeft = levelData.StartingLives;
        
        loot1 += () => Debug.Log("loot 1");
        loot1 += () => Loot();
        loot1 += () => Timer.Create(loot2, levelData.LootTimes[1], "SecondWave");
        loot2 += () => Debug.Log("loot 2");
        loot2 += () => Loot();
    }

    private void Loot()
    {
         // = lm.DrawThreeItems();
        // _ui.InstantiateLootMenu();
    }

    private void OnLoose()
    {
        //set highscores
    }
}