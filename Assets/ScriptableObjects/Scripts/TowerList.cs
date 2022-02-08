using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Tower List", menuName = "ScriptableObjects/Towers/Tower List", order = 0)]
    public class TowerList : ScriptableObject
    {
        [SerializeField] private List<TowerData> towersList;
        public List<TowerData> TowersList => towersList;
    }
}