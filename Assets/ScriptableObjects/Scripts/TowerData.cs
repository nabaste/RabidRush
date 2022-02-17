using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Objects/Towers/Tower Stats Data", order = 1)]
    public class TowerData : ScriptableObject
    {
        #region Display

        [Header("Display Properties")] 
        [SerializeField] private string towerName;
        public string TowerName => towerName;
        [SerializeField] private Sprite towerIcon;
        public Sprite TowerIcon => towerIcon;
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        #endregion

        #region BasicStats

        [Header("Base Stats")] [SerializeField]
        private float life;
        public float Life => life;
        [SerializeField] private float range;
        public float Range => range;
        [SerializeField] private float damage;
        public float Damage => damage;
        [SerializeField] private int cooldown;
        public int Cooldown => cooldown;
        [SerializeField] private float rotationSpeed;
        public float RotationSpeed => rotationSpeed;
        [SerializeField] private float angleFOV;
        public float AngleFOV => angleFOV;

        #endregion

        #region BuyAndSellStats

        [Header("Buy & Sell Stats")] [SerializeField]
        private int cost;

        public int Cost => cost;
        [SerializeField] private float sellPricePercentage;
        public float SellPricePercentage => sellPricePercentage;
        [SerializeField] private float upgradeCost;
        public float UpgradeCost => upgradeCost;

        #endregion

        #region Utilities

        [Header("Utilities")] 

        [SerializeField] private float placerSizeX;
        public float PlacerSizeX => placerSizeX;
        [SerializeField] private float placerSizeZ;
        public float PlacerSizeZ => placerSizeZ;

        #endregion
    }
}