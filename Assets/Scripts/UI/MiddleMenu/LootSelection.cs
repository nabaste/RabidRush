using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class LootSelection : SideMenu
{
    [SerializeField] private VisualTreeAsset lootListItem;
    private VisualElement _lootListContainer;

    [SerializeField] private MiddleMenu mm;


    private void Start()
    {
        _lootListContainer = Root.Q<VisualElement>("loot-list-container");
        for (int i = 0; i < LevelManager.Instance.lootEvents.Count; i++)
        {
            LevelManager.Instance.lootEvents[i] += BuildLootList;
        }
    }

    private void BuildLootList(List<Item> loots)
    {
        Time.timeScale = 0;
        foreach (var loot in loots)
        {
            VisualElement listItem = lootListItem.Instantiate();
            _lootListContainer.Add(listItem);
            listItem.RegisterCallback<ClickEvent>(ev => OnItemPick(loot));
            var text = listItem.Q<Label>("loot-list-item-label");
            text.text = loot.ItemName;
        }
    }

    private void OnItemPick(Item selected) 
    {
        LevelManager.Instance.pd.Inventory.itemList.Add(selected);
        mm.HideMiddleMenu();
        _lootListContainer.Clear();
        Time.timeScale = 1;
    }
}