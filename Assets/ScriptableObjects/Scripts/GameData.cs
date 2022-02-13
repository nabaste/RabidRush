using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "Scriptable Objects/Game Data", order = 6)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private float[] easyLootRates;
        public float[] EasyLootRates => easyLootRates;
        [SerializeField] private float[] mediumLootRates;
        public float[] MediumLootRates => mediumLootRates;
        [SerializeField] private float[] hardLootRates;
        public float[] HardLootRates => hardLootRates;
        public List<float[]> LootRates { get; } = new List<float[]>();

        private void Start()
        {
            LootRates.Add(easyLootRates);
            LootRates.Add(mediumLootRates);
            LootRates.Add(hardLootRates);
        }
    }
}