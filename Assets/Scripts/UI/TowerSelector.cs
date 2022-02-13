using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class TowerSelector : SideMenu
{
    [SerializeField] private VisualTreeAsset towerListItem;
    [SerializeField] private TowerList towers;

    private VisualElement _towerListContainer;


    private void Start()
    {
        _towerListContainer = Root.Q<VisualElement>("tower-list");
        

        BuildTowerList();
    }

    private void BuildTowerList()
    {
        foreach (var t in towers.TowersList)
        {
            VisualElement listItem = towerListItem.Instantiate();
            _towerListContainer.Add(listItem);
            var text = listItem.Q<Label>("tower-list-item-label");
            text.text = t.TowerName;
        }
    }
}