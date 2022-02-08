using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Zombie List", menuName = "ScriptableObjects/Zombies/Zombie List", order = 1)]
    public class ZombieList : ScriptableObject
    {
        public List<ZombieData> zombies = new List<ZombieData>();
    }
}