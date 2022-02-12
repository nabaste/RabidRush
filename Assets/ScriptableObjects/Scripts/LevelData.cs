using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Levels/Level Data ", order = 1)]
    public class LevelData : ScriptableObject
    {
        // [SerializeField] private List<Vector3> startLocations = new List<Vector3>();
        // public List<Vector3> StartLocations => startLocations;
        [SerializeField] private StartPositionData[] startPositions;
        public StartPositionData[] StartLocations => startPositions;
        [SerializeField] private Vector3 endPosition;
        public Vector3 EndPosition => endPosition;
        [SerializeField] private List<float> lootTimes = new List<float>();
        public List<float> LootTimes => lootTimes;
        [SerializeField] private int[] budgets;
        public int[] Budgets => budgets;
        [SerializeField] private int startingKarts;
        public int StartingKarts => startingKarts;
        [SerializeField] private int startingLives;
        public int StartingLives => startingLives;
        [SerializeField] private ItemList availableItems;
        public ItemList AvailableItems => availableItems;
    }
}