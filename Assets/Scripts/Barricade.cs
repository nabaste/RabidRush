using System;
using System.Collections;
using System.Collections.Generic;
using RabidRush.Towers;
using UnityEngine;
using UnityEngine.AI;

public class Barricade : MonoBehaviour, IDamageable, IPurchaseable, IInspectable
{
    [SerializeField] private float life;
    [SerializeField] private int cost;
    private bool _placed = false;
    private BoxCollider _collider;
    private NavMeshObstacle _navMeshObstacle;

    [SerializeField] private PlacementManager placementManager;

    public Action OnDestroyed;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        
        placementManager.Build(2, 1, "Walkway");
        placementManager.OnPlacement += OnPlacementHandler;
    }

    private void OnPlacementHandler()
    {
        _collider.enabled = true;
        _navMeshObstacle.enabled = true;
        _placed = true;
    }

    private void Update()
    {
        if(_placed) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.rotation = Quaternion.Euler(0, 90f, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void GetDamage(float dmg, kindOfDamage kind, float pace = 0, float duration = 0)
    {
        life -= dmg;
        CheckIfDestroyed();
    }
    public int GetCost()
    {
        return cost;
    }
    private void CheckIfDestroyed()
    {
        if (life > 0) return;
        Destroy(gameObject);
        OnDestroyed?.Invoke();
    }

    public string GetName()
    {
        return name;
    }

    public float GetLife()
    {
        return life;
    }

    public Dictionary<string, float> GetStats()
    {
        return new Dictionary<string, float>();
    }
}
