using UnityEngine;
using UnityEngine.UIElements;

public class TimeFlowControl : UpperMenu
{
    private Button _menuButton;
    private Button _playPauseButton;
    private Button _ffButton;

    public void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        
        _menuButton = Root.Q<Button>("menu-button");
        _playPauseButton = Root.Q<Button>("play-pause-button");
        _ffButton = Root.Q<Button>("ff-button");

        _menuButton.RegisterCallback<ClickEvent>(ev => OnMenuButtonClick());
        _playPauseButton.RegisterCallback<ClickEvent>(ev => OnPlayPauseButtonClick());
        _ffButton.RegisterCallback<ClickEvent>(ev => OnFFButtonClick());
    }
    private void OnMenuButtonClick()
    {
        Debug.Log("Menu");
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