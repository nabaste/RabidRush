using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MiddleMenu : SideMenu
{
    private VisualElement _middleMenuOnDisplay;
    private VisualElement _inspector;
    private VisualElement _lootSelection;

    private Label _unitName;
    private Label _unitStats;

    private void Start()
    {
        _inspector = Root.Q<VisualElement>("inspector");
        _lootSelection = Root.Q<VisualElement>("loot-selector");
        
        _unitName = Root.Q<Label>("inspector-unit-name");
        _unitStats = Root.Q<Label>("inspector-unit-stats");

        for (int i = 0; i < LevelManager.Instance.lootEvents.Count; i++)
        {
            LevelManager.Instance.lootEvents[i] += list => MenuChange(_lootSelection, true);    
        }
        
        
    }

    public void MenuChange(VisualElement menu, bool hide)
    {
        if (_middleMenuOnDisplay == _lootSelection && menu != _lootSelection) return;
        if (_middleMenuOnDisplay != null) _middleMenuOnDisplay.style.display = DisplayStyle.None;
        if (_middleMenuOnDisplay == menu && hide)
        {
            // _middleMenuOnDisplay = null;
            HideMiddleMenu();
            return;
        }

        menu.style.display = DisplayStyle.Flex;

        _middleMenuOnDisplay = menu;
    }

    public void BuildInspectorMenu(IInspectable inspected)
    {
        
        //...
        _unitName.text = inspected.GetName();
        _unitStats.text = inspected.GetLife().ToString();
        Debug.Log(inspected.GetName());
        MenuChange(_inspector, false);
    }

    public void HideMiddleMenu()
    {
        _middleMenuOnDisplay.style.display = DisplayStyle.None;
        _middleMenuOnDisplay = null;
    }
}