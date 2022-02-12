using System;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObjects/Players/Player Data", order = 0)]
    [Serializable]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private string playerName;
        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }

        [SerializeField] private int kartAmount;
        public int KartAmount
        {
            get => kartAmount;
            set => kartAmount = value;
        }

        [SerializeField] private DifficultyLevel level;

        public DifficultyLevel Level
        {
            get => level;
            set => level = value;
        }

        [SerializeField] private ItemList inventory;
        public ItemList Inventory => inventory;
    }
}