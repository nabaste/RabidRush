
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;


public class KartCounter : SideMenu
{
    [SerializeField] private PlayerData pd;
    private Label kartAmount;

    private void Start()
    {
        // Root = GetComponent<UIDocument>().rootVisualElement;
        
        kartAmount = Root.Q<Label>("kart-amount-number");
        Refresh();
    }

    public void Refresh()
    {
        kartAmount.text = pd.KartAmount.ToString();
    }
}