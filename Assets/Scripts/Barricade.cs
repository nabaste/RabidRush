using System;
using System.Collections;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using RabidRush.Towers;
using UnityEngine;
using UnityEngine.AI;

public class Barricade : MonoBehaviour, IDamageable, IPurchaseable, IInspectable
{
    [SerializeField] private BarricadeData bd;
    private float _life;
    private BoxCollider _collider;
    private NavMeshObstacle _navMeshObstacle;

    [SerializeField] private PlacementManager placementManager;

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
}
