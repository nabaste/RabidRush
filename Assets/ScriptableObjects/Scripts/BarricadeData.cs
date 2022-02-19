using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Barricade Data", menuName = "Scriptable Objects/Special Attacks/Barricade Data", order = 0)]
    public class BarricadeData : ScriptableObject
    {
        [SerializeField] private float life;
        public float Life => life;
        [SerializeField] private int cost;
        public int Cost => cost;
        [SerializeField] private float sellPricePercentage;
        public float SellPricePercentage => sellPricePercentage;
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}