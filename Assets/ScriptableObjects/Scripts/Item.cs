using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Items/Item", order = 0)]
    public class Item : ScriptableObject
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
        public TowerStats StatToImprove => statToImprove;
        [SerializeField] private float changePercentage;
        public float ChangePercentage => changePercentage;
        [SerializeField] private global::Towers wearer;
        public global::Towers Wearer => wearer;
    }
}