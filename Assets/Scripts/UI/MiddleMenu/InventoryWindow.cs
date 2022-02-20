using System;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class InventoryWindow : SideMenu
{
    [SerializeField] private VisualTreeAsset inventoryItem;
    private VisualElement _inventoryItemsContainer;

    private void Start()
    {
        _inventoryItemsContainer = Root.Q<VisualElement>("inventory-item-container");

        BuildInventory();
    }

    private void BuildInventory()
    {
        foreach (var item in LevelManager.Instance.pd.Inventory.itemList)
        {
            VisualElement slot = inventoryItem.Instantiate();
            _inventoryItemsContainer.Add(slot);
            
            var displayImg = item.Img;
            slot.style.backgroundImage = new StyleBackground(displayImg);

            slot.tooltip = item.Description;
            slot.AddManipulator(new ToolTipManipulator());
        }
    }
}