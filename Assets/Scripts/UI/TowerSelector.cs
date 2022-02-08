using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class TowerSelector : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset towerListItem;
    [SerializeField] private TowerList towers;

    private VisualElement _root;
    private VisualElement _towerListContainer;

    private List<VisualElement> _instantiatedListItems = new List<VisualElement>();

    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _towerListContainer = _root.Q<VisualElement>("tower-list");
        

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