using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class TowerSelector : SideMenu
{
    [SerializeField] private MiddleMenu mm;
    
    [SerializeField] private VisualTreeAsset towerListItem;
    [SerializeField] private TowerList towers;
    [SerializeField] private Transform towerContainer;
    
    private VisualElement _towerSelector;
    private VisualElement _towerListContainer;

    private void Start()
    {
        _towerSelector = Root.Q<VisualElement>("tower-selector");
        _towerListContainer = Root.Q<VisualElement>("tower-list");

        BuildTowerList();
    }

    private void BuildTowerList()
    {
        foreach (var t in towers.TowersList)
        {
            VisualElement listItem = towerListItem.Instantiate();
            _towerListContainer.Add(listItem);
            listItem.RegisterCallback<ClickEvent>(evt => PlaceTower(t));
            var text = listItem.Q<Label>("tower-list-item-label");
            text.text = t.TowerName;
            
            //Check if player has sufficient funds
        }
    }

    private void PlaceTower(TowerData tower)
    {
        Instantiate(tower.Prefab, towerContainer);
        mm.MenuChange(_towerSelector, true);
    }
}