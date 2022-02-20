using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using RabidRush.ScriptableObjects;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelList levelList;
    public LevelData levelData;
    [SerializeField] public PlayerData pd;
    [SerializeField] public GameData gd;

    [SerializeField] private KartCounter kc;
    private int _kartAmount;

    public int KartAmount
    {
        get => _kartAmount;
        set
        {
            _kartAmount = value;
            kc.Refresh();
        }
    }

    public int livesLeft;

    [SerializeField] public LootManager lm;
    public event Action<List<Item>> Loot0;
    public event Action<List<Item>> Loot1;
    public event Action<List<Item>> Loot2;

    public List<Action<List<Item>>> lootEvents;

    public VisualElement RootVisualElement;

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

        lootEvents = new List<Action<List<Item>>>(levelData.LootTimes.Count)
        {
            Loot0,
            Loot1,
            Loot2
        };
    }

    private void Start()
    {
        KartAmount = levelData.StartingKarts;
        livesLeft = levelData.StartingLives;
    }

    public void StartLootCountdown()
    {
        StartCoroutine(SearchingForLoot());
    }

    private IEnumerator SearchingForLoot()
    {
        for (int i = 0; i < levelData.LootTimes.Count; i++)
        {
            yield return new WaitForSeconds(levelData.LootTimes[i]);
            lootEvents[i].Invoke(GetLoot());
        }
    }

    private List<Item> GetLoot()
    {
        var lootOptions = new List<Item>();
        // var ratesToUse;
        var ratesToUse = new float[4];
        switch (pd.Level)
        {
            case DifficultyLevel.Easy:
                ratesToUse = gd.EasyLootRates;
                break;
            case DifficultyLevel.Medium:
                ratesToUse = gd.MediumLootRates;
                break;
            case DifficultyLevel.Hard:
                ratesToUse = gd.HardLootRates;
                break;
            default:
                break;
        }

        lootOptions = lm.DrawThreeItems(levelData.AvailableItems.itemList, ratesToUse);
        return lootOptions;
    }

    private void OnLoose()
    {
        //Show loosing screen
        //set highscores
    }

    private void OnWin()
    {
        //Show winning screen
    }
}