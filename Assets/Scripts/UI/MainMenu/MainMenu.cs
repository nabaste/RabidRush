using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenu : MonoBehaviour
{
    public VisualElement Root;

    private Button _startGameButton;
    private Button _optionsButton;
    private Button _exitButton;

    private void OnEnable()
    {
        Root = GetComponentInParent<UIDocument>().rootVisualElement;

        _startGameButton = Root.Q<Button>("main-menu-start-game");
        _optionsButton = Root.Q<Button>("main-menu-options-button");
        _exitButton = Root.Q<Button>("main-menu-quit-button");

        _startGameButton.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene(2));
        
        _exitButton.RegisterCallback<ClickEvent>(evt => OnQuitGame());
    }


        private void OnQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }