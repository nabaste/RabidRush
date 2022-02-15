using System;
using UnityEngine;
using UnityEngine.UIElements;

    public class MenuWindow : MonoBehaviour
    {
        protected VisualElement Root;

        private VisualElement _menuWindow;
        private Button _resumeButton;
        private void OnEnable()
        {
            Root = GetComponentInParent<UIDocument>().rootVisualElement;
        }

        private void Start()
        {
            _menuWindow = Root.Q<VisualElement>("Menu-Window");
            _resumeButton = Root.Q<Button>("menu-window-resume-button");
            
            _resumeButton.RegisterCallback<ClickEvent>(ev => OnResumeButtonClick());
        }

        private void OnResumeButtonClick()
        {
            Time.timeScale = 1;
            _menuWindow.style.display = DisplayStyle.None;
        }
    }