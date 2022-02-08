using UnityEngine;
using UnityEngine.UIElements;

public class SelectorButtons : UpperMenu
{
    private Button _inventoryButton;
    private Button _towerSelectionButton;
    
    private VisualElement _inventory;
    private VisualElement _towerSelector;

    [SerializeField] private MiddleMenu middleMenu;
    
   

    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        
        _inventoryButton = Root.Q<Button>("inventory-button");
        _towerSelectionButton = Root.Q<Button>("towers-button");
        
        _inventory = Root.Q<VisualElement>("inventory");
        
        _towerSelector = Root.Q<VisualElement>("tower-selector");
        
        _inventoryButton.RegisterCallback<ClickEvent>(ev => middleMenu.MenuChange(_inventory, true));
        _towerSelectionButton.RegisterCallback<ClickEvent>(ev => middleMenu.MenuChange(_towerSelector, true));
    }
    
   
}