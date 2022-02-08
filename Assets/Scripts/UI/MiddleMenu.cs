using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MiddleMenu : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _middleMenuOnDisplay;
    private VisualElement _inspector;

    private Label _unitName;
    private Label _unitStats;

    private void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _inspector = _root.Q<VisualElement>("inspector");

        _unitName = _root.Q<Label>("inspector-unit-name");
        _unitStats = _root.Q<Label>("inspector-unit-stats");
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