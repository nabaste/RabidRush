using System;
using RabidRush.ScriptableObjects;
using RabidRush.Towers;
using RabidRush.Zombies;
using UnityEngine;
using UnityEngine.UIElements;


public class ActionButtons : SideMenu
{
    private ItemList _inventory;
    private Button _button0;
    private Button _button1;

    private void Awake()
    {
        _inventory = LevelManager.Instance.pd.Inventory;
    }

    public bool CheckIfUpgradeAvailable(Towers wearer)
    {
        foreach (var item in _inventory.itemList)
        {
            if (item.Wearer == wearer) return true;
        }

        return false;
    }


    public void BuildButtons()
    {
        
    }
}