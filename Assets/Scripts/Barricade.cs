using System;
using System.Collections;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using RabidRush.Towers;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Barricade : MonoBehaviour, IDamageable, IPurchaseable, IInspectable
{
    [SerializeField] private BarricadeData bd;
    private float _life;
    private BoxCollider _collider;
    private NavMeshObstacle _navMeshObstacle;

    [SerializeField] private PlacementManager placementManager;

    private int _totalKarts;
    private int _currentKarts;
    public event Action OnDestroyed;
    public event Action OnSell;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        
        placementManager.Build(1.8f, 1, "Walkway");
        placementManager.OnPlacement += OnPlacementHandler;
        placementManager.OnPlacement += PayForBarricade;

        _life = bd.Life;
        
        _totalKarts = transform.childCount;
        _currentKarts = _totalKarts;
        
        OnSell += SellBarricade;
        OnSell += OnDestroyed;
    }

    private void OnPlacementHandler()
    {
        _collider.enabled = true;
        _navMeshObstacle.enabled = true;
    }

    private void PayForBarricade()
    {
        LevelManager.Instance.KartAmount -= bd.Cost;
    }

    private void SellBarricade()
    {
        LevelManager.Instance.KartAmount += Mathf.RoundToInt(bd.Cost * bd.SellPricePercentage * (_life/bd.Life));
        Destroy(gameObject);
    }
    
    public void GetDamage(float dmg, kindOfDamage kind, float pace = 0, float duration = 0)
    {
        _life -= dmg;
        CheckIfDestroyed();
        ShowDamage();
    }
    public int GetCost()
    {
        return bd.Cost;
    }
    private void CheckIfDestroyed()
    {
        if (_life > 0) return;
        Destroy(gameObject);
        OnDestroyed?.Invoke();
    }

    public string GetName()
    {
        return name;
    }

    public float GetLife()
    {
        return _life;
    }

    public Dictionary<string, float> GetStats()
    {
        var stats = new Dictionary<string, float>();
        stats.Add("life", _life);
        return stats;
    }
    public Dictionary<string, Action> GetPossibleActions()
    {
        var actions = new Dictionary<string, Action>();
        int sellPrice = Mathf.RoundToInt(bd.Cost * bd.SellPricePercentage * (_life / bd.Life));
        actions.Add($"sell for {sellPrice}", OnSell);
        return actions;
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    private void ShowDamage()
    {
        var dmgPer = Mathf.RoundToInt(_life / bd.Life);
        var kartsToRemove = _currentKarts - dmgPer;
        
        for (int i = 0; i < kartsToRemove; i++)
        {
            var kartToDestroy = transform.GetChild(Random.Range(0, _currentKarts-1));
            Destroy(kartToDestroy.gameObject);
            _currentKarts--;
        }
    }
}
