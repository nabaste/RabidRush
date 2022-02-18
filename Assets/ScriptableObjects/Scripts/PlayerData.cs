using System;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Objects/Players/Player Data", order = 0)]
    
    [Serializable]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private string playerName;
        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }

        [SerializeField] private DifficultyLevel level;

        public DifficultyLevel Level
        {
            get => level;
            set => level = value;
        }

        [SerializeField] private ItemList inventory;
        public ItemList Inventory => inventory;

        [Range(0,1)]
        [SerializeField] private float snappingStrength;
        public float SnappingStrength
        {
            get => snappingStrength;
            set => snappingStrength = value;
        }
        
    }
}