using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
public class TimeCounter : MonoBehaviour
{
    private VisualElement Root;
    private Label _timerForeground;

    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;

        _timerForeground = Root.Q<Label>("timer-foreground");
        _timerForeground.RegisterCallback<ClickEvent>(ev => AnimateTimer());
    }

    private void AnimateTimer()
    {
        _timerForeground.text = "";
        DOTween.To(() => 100, x => _timerForeground.style.height = x, 0, 12f).SetEase(Ease.Linear);
        DOTween.To(() => 100, x => _timerForeground.style.width = x, 0, 12f).SetEase(Ease.Linear);
    }
}