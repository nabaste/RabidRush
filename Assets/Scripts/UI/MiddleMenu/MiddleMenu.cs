using System;
using System.Collections.Generic;
using System.Diagnostics;
using RabidRush.ScriptableObjects;
using RabidRush.Towers;
using RabidRush.Zombies;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MiddleMenu : SideMenu
{
    private VisualElement _middleMenuOnDisplay;
    private VisualElement _inspector;
    private VisualElement _lootSelection;
    private Label _unitName;
    
    [SerializeField] private InspectorCamera inspectorCamera;
    private Image _inspectorCameraView;
    [SerializeField] private CustomRenderTexture tex;

    [SerializeField] private VisualTreeAsset statListItem;
    private VisualElement _statListContainer;
    [SerializeField] private VisualTreeAsset actionButton;
    private VisualElement _actionsContainer;

    [SerializeField] private UpgradesList ul;

    private void Start()
    {
        _inspector = Root.Q<VisualElement>("inspector");
        _lootSelection = Root.Q<VisualElement>("loot-selector");

        _unitName = Root.Q<Label>("inspector-unit-name");
        // _unitStats = Root.

        for (int i = 0; i < LevelManager.Instance.lootEvents.Count; i++)
        {
            LevelManager.Instance.lootEvents[i] += list => MenuChange(_lootSelection, true);
        }

        _inspectorCameraView = Root.Q<Image>("camera-selected-item");

        _statListContainer = Root.Q<VisualElement>("stat-items-container");
        _actionsContainer = Root.Q<VisualElement>("actions-container");

        SetUpInspectorImage();
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

        if (_middleMenuOnDisplay == _inspector && menu != _inspector)
        {
            inspectorCamera.enabled = false;
            ul.HideUpgradeWindow();
        }

        menu.style.display = DisplayStyle.Flex;

        _middleMenuOnDisplay = menu;
    }

    public void BuildInspectorMenu(IInspectable inspected)
    {
        inspectorCamera.SetTarget(inspected);
        inspectorCamera.enabled = true;
        _unitName.text = inspected.GetName();
        
        _statListContainer.Clear();
        _actionsContainer.Clear();
        
        foreach (var stat in inspected.GetStats())
        {
            VisualElement listItem = statListItem.Instantiate();
            _statListContainer.Add(listItem);
            Label textElement = listItem.Q<Label>("inspector-unit-stats");
            textElement.text = stat.Key + ":" + stat.Value;
        }

        foreach (var action in inspected.GetPossibleActions())
        {
            VisualElement buttonContainer = actionButton.Instantiate();
            _actionsContainer.Add(buttonContainer);
            Button button = buttonContainer.Q<Button>("action-button");
            button.text = action.Key;
            button.RegisterCallback<ClickEvent>(evt =>
            {
                action.Value?.Invoke();
                if (action.Key ==
                    $"Upgrade for {inspected.GetTransform().gameObject.GetComponent<TowerModel>().towerData.UpgradeCost}")
                {
                    ul.BuildUpgradeOptions(inspected.GetTransform().gameObject.GetComponent<TowerModel>());
                }
                else
                {
                    HideMiddleMenu();
                }
            });
        }

        MenuChange(_inspector, false);
    }

    public void HideMiddleMenu()
    {
        _middleMenuOnDisplay.style.display = DisplayStyle.None;
        _middleMenuOnDisplay = null;
    }

    private void SetUpInspectorImage()
    {
        _inspectorCameraView.image = tex;
    }

    
}