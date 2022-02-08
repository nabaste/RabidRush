using UnityEngine;

using RabidRush.ScriptableObjects;

[System.Serializable]
public class Item
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    [SerializeField] private int id;
    public int Id => id;
    [TextArea(4, 10)] [SerializeField] private string description;
    public string Description => description;
    [SerializeField] private ItemRarity rarity;
    public ItemRarity Rarity => rarity;
    [SerializeField] private Sprite img;
    public Sprite Img => img;
    [SerializeField] private TowerStats statToImprove;
    public TowerStats StattoImpove => statToImprove;
    [SerializeField] private float changePercentage;
    public float ChangePercentage => changePercentage;
    [SerializeField] private TowerList wearer;
    public TowerList Wearer => wearer;
}