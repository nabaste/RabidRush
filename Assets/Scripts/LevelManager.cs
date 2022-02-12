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
        }
    }
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