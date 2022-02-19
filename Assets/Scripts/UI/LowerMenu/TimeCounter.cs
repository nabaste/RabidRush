using System;
using System.Linq;
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
        startEv += evt => LevelManager.Instance.StartLootCountdown();

        _timerForeground = Root.Q<Label>("timer-foreground");
        _timerForeground.RegisterCallback(startEv);
    }

    private void AnimateTimer()
    {
        var duration = LevelManager.Instance.levelData.LootTimes.Sum();
        _timerForeground.text = "";
        DOTween.To(() => 100, x => _timerForeground.style.height = x, 0, duration).SetEase(Ease.Linear);
        DOTween.To(() => 100, x => _timerForeground.style.width = x, 0, duration).SetEase(Ease.Linear);
    }
}