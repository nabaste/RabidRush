
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class KartCounter : SideMenu
{
    private Label kartAmount;

    private void Start()
    {
        
        kartAmount = Root.Q<Label>("kart-amount-number");
        Refresh();
    }

    public void Refresh()
    {
        kartAmount.text = LevelManager.Instance.money.ToString();
    }
}