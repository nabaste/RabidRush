using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MiddleMenu : SideMenu
{
    private VisualElement _middleMenuOnDisplay;
    private VisualElement _inspector;

    private Label _unitName;
    private Label _unitStats;

    private void Start()
    {
        _inspector = Root.Q<VisualElement>("inspector");

        _unitName = Root.Q<Label>("inspector-unit-name");
        _unitStats = Root.Q<Label>("inspector-unit-stats");
    }

    public void MenuChange(VisualElement menu, bool hide)
    {
        if (_middleMenuOnDisplay != null) _middleMenuOnDisplay.style.display = DisplayStyle.None;
        if (_middleMenuOnDisplay == menu && hide)
        {
            _middleMenuOnDisplay = null;
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
}