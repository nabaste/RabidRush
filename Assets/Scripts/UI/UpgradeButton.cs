using System;
using RabidRush.ScriptableObjects;
using UnityEngine;


public class UpgradeButton : SideMenu
{
    private ItemList _inventory;

    private void Awake()
    {
        _inventory = LevelManager.Instance.pd.Inventory;
    }
}