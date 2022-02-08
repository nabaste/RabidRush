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
    [SerializeField] private int money;
    // [SerializeField] private UIHandler _ui;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            // _ui.UpdateUI("money");
        }
    }
    private int enemiesKilled = 0;
    public int EnemiesKilled
    {
        get
        {
            return enemiesKilled;
        }
        set
        {
            enemiesKilled = value;
            // _ui.UpdateUI("enemies");
        }
    }
    [SerializeField] private int livesLeft;
    public int LivesLeft
    {
        get
        {
            return livesLeft;
        }
        set
        {
            livesLeft = value;
            // _ui.UpdateUI("lives");
        }
    }
    // [SerializeField] private Controller controller;
    // public Controller Controller
    // {
    //     get
    //     {
    //         return controller;
    //     }
    // }
    public Action startOfWaves;
    [SerializeField] public LootManager lm;
    private Action loot1;
    private Action loot2;
    public List<Item> lootOptions = new List<Item>(); 
    #region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Scene Manager is Null!");
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
        startOfWaves += () => Timer.Create(loot1, levelData.LootTimes[0], "FirstWave");
        loot1 += () => Debug.Log("loot 1");
        loot1 += () => Loot();
        loot1 += () => Timer.Create(loot2, levelData.LootTimes[1], "SecondWave");
        loot2 += () => Debug.Log("loot 2");
        loot2 += () => Loot();
    }
    private void Update()
    {
        if (livesLeft <= 0)
        {
            Debug.Log("lost");
        }
    }
    private void Loot()
    {
        lootOptions = lm.DrawThreeItems();
        // _ui.InstantiateLootMenu();
    }
    void OnLoose()
    {
        //set highscores
    }
}