using UnityEngine;
using UnityEngine.UIElements;

public class TimeFlowControl : SideMenu
{
    private Button _menuButton;
    private Button _playPauseButton;
    private Button _ffButton;

    [SerializeField] private VisualElement pauseMenu;
    // [SerializeField] private VisualTreeAsset pauseMenu; 
    public void Start()
    {
        
        _menuButton = Root.Q<Button>("menu-button");
        _playPauseButton = Root.Q<Button>("play-pause-button");
        _ffButton = Root.Q<Button>("ff-button");

        _menuButton.RegisterCallback<ClickEvent>(ev => OnMenuButtonClick());
        _playPauseButton.RegisterCallback<ClickEvent>(ev => OnPlayPauseButtonClick());
        _ffButton.RegisterCallback<ClickEvent>(ev => OnFFButtonClick());

        pauseMenu = Root.Q<VisualElement>("menu-container");
    }
    private void OnMenuButtonClick()
    {
        Debug.Log("Menu");
        Time.timeScale = 0;
        pauseMenu.style.display = DisplayStyle.Flex;
        // var menu = pauseMenu.Instantiate();
        // Root.Add(menu);
        // menu.style.position.value = Position.Absolute;


    }
    private void OnPlayPauseButtonClick()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
    }
    private void OnFFButtonClick()
    {
        Time.timeScale = Time.timeScale == 1f? 2f : 1f;
    }
}