using RabidRush.ScriptableObjects;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Chemical Tower Data", menuName = "Scriptable Objects/Towers/Chemical Tower Stats Data", order = 2)]
    public class ChemicalTowerData : TowerData
    {
        [Header("Chemical Stats")]
        [SerializeField] private float pace;
        public float Pace => pace;
        [SerializeField] private float duration;
        public float Duration => duration;
    }
}