using System;
using UnityEngine;
using UnityEngine.UIElements;

    public class MenuWindow : MonoBehaviour
    {
        protected VisualElement Root;

        private VisualElement _menuContainer;
        private Button _resumeButton;
        private void OnEnable()
        {
            Root = GetComponentInParent<UIDocument>().rootVisualElement;
        }

        private void Start()
        {
            _menuContainer = Root.Q<VisualElement>("menu-container");
            _resumeButton = Root.Q<Button>("menu-window-resume-button");
            
            _resumeButton.RegisterCallback<ClickEvent>(ev => OnResumeButtonClick());
        }

        private void OnResumeButtonClick()
        {
            Time.timeScale = 1;
            _menuContainer.style.display = DisplayStyle.None;
        }
    }