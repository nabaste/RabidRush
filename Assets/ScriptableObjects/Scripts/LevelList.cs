using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level List", menuName = "ScriptableObjects/Levels/Level List", order = 0)]
    public class LevelList : ScriptableObject
    {
        public List<LevelData> levels = new List<LevelData>();
    }
}