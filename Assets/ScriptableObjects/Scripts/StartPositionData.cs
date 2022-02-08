using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Start Position Data", menuName = "ScriptableObjects/Levels/Start Position Data",
        order = 1)]
    public class StartPositionData : ScriptableObject
    {
        [SerializeField] private Vector3 position;
        public Vector3 Position => position;
        [Range(0,1)]
        [SerializeField] private float priority;
        public float Priority
        {
            get => priority;
            set => priority = value;
        }

        public ZombieData[] zombieDatas;
        [Range(0,1)]
        public float[] zombieChances;
        public Dictionary<ZombieData, float> ZombieComposition = new Dictionary<ZombieData, float>();

    
        void MakeDictionary()
        {
            for (int i = 0; i < zombieDatas.Length; i++)
            {
                ZombieComposition.Add(zombieDatas[i], zombieChances[i]);
            }
        }

        private void OnEnable()
        {
            MakeDictionary();
        }
    }
}