using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class LootSelection : SideMenu
{
    [SerializeField] private VisualTreeAsset lootListItem;
    private VisualElement _lootListContainer;


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
        foreach (var loot in loots)
        {
            VisualElement listItem = lootListItem.Instantiate();
            _lootListContainer.Add(listItem);
            var text = listItem.Q<Label>("loot-list-item-label");
            text.text = loot.ItemName;
        }
    }
}