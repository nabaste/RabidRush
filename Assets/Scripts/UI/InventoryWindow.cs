using System;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class InventoryWindow : SideMenu
{
    [SerializeField] private VisualTreeAsset inventoryItem;
    [SerializeField] private Items inventory;

    // private VisualElement _root;
    private VisualElement _inventoryItemsContainer;

    private void Start()
    {
        // Root = GetComponent<UIDocument>().rootVisualElement;
        _inventoryItemsContainer = Root.Q<VisualElement>("inventory-item-container");

        BuildInventory();
    }

    private void BuildInventory()
    {
        foreach (var item in inventory.items)
        {
            VisualElement slot = inventoryItem.Instantiate();
            _inventoryItemsContainer.Add(slot);
            var displayImg = item.Img;
            slot.style.backgroundImage = new StyleBackground(displayImg);
        }
    }
}