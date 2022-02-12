using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeCounter : SideMenu
{
    private Label _timerForeground;

    public EventCallback<ClickEvent> startEv;
    [SerializeField] private WavesParametersGenerator wpg;

    private void Start()
    {

        startEv += evt => AnimateTimer();
        startEv += evt => wpg.EnableSpawners();

        _timerForeground = Root.Q<Label>("timer-foreground");
        _timerForeground.RegisterCallback(startEv);
    }

    private void AnimateTimer()
    {
        _timerForeground.text = "";
        DOTween.To(() => 100, x => _timerForeground.style.height = x, 0, 12f).SetEase(Ease.Linear);
        DOTween.To(() => 100, x => _timerForeground.style.width = x, 0, 12f).SetEase(Ease.Linear);
    }
}