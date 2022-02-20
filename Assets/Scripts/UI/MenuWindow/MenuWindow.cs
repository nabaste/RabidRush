using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

    public class MenuWindow : MonoBehaviour
    {
        protected VisualElement Root;

        private VisualElement _menuContainer;
        private Button _resumeButton;
        private Button _mainMenuButton;
        private void OnEnable()
        {
            Root = GetComponentInParent<UIDocument>().rootVisualElement;
        }

        private void Start()
        {
            _menuContainer = Root.Q<VisualElement>("menu-container");
            _resumeButton = Root.Q<Button>("menu-window-resume-button");
            _mainMenuButton = Root.Q<Button>("menu-window-mainmenu-button");


            
            _resumeButton.RegisterCallback<ClickEvent>(ev => OnResumeButtonClick());

            _mainMenuButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(0));
        }

        private void OnResumeButtonClick()
        {
            Time.timeScale = 1;
            _menuContainer.style.display = DisplayStyle.None;
        }
    }