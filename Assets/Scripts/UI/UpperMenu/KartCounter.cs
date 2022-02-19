
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class KartCounter : SideMenu
{
    private Label _kartAmount;

    private void OnEnable()
    {
        Root = GetComponentInParent<UIDocument>().rootVisualElement;
        _kartAmount = Root.Q<Label>("kart-amount-number");
        Refresh();
    }

    public void Refresh()
    {
        _kartAmount.text = LevelManager.Instance.KartAmount.ToString();
    }
    
}