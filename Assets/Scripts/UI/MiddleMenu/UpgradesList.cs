using System;
using RabidRush.ScriptableObjects;
using RabidRush.Towers;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradesList : SideMenu
{
    [SerializeField] private VisualTreeAsset upgradeItem;
    private ItemList inventory;
    private VisualElement _listContainer;
    private VisualElement _upgradesWindow;

    private void Start()
    {
        _listContainer = Root.Q<VisualElement>("upgrade-list");
        _upgradesWindow = Root.Q<VisualElement>("upgrade-selector");
        inventory = LevelManager.Instance.pd.Inventory;
    }

    public void BuildUpgradeOptions(TowerModel tower)
    {
        _listContainer.Clear();
        _upgradesWindow.style.display = DisplayStyle.Flex;

        int validOptions = 0;
        foreach (var item in inventory.itemList)
        {
            if (item.Wearer == tower.towerData.Kind)
            {
                validOptions++;
                VisualElement listItem = upgradeItem.Instantiate();
                _listContainer.Add(listItem);
                Label text = listItem.Q<Label>("upgrade-list-item-label");
                text.text = item.name;
                
                listItem.RegisterCallback<ClickEvent>(evt =>
                {
                    tower.Upgrade(item);
                    _upgradesWindow.style.display = DisplayStyle.None;
                });
            }
        }

        if (validOptions != 0) return;
        Label warning = new Label("You don't have any items to upgrade this character");
        _listContainer.Add(warning);
    }

    public void HideUpgradeWindow()
    {
        _upgradesWindow.style.display = DisplayStyle.None;
    }
}