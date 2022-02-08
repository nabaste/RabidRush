using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Zombie Data", menuName = "ScriptableObjects/Zombies/Zombie Stats Data", order = 0)]
    public class ZombieData : ScriptableObject
    {
        [Header("Base Stats")]
        [SerializeField] private float speed;
        public float Speed => speed;
        [SerializeField] private float life;
        public float Life => life;
        [SerializeField] private int attackDamage;
        public int AttackDamage => attackDamage;
        [SerializeField] private float attackCooldown;
        public float AttackCooldown => attackCooldown;

        [Header("Pooling Data")] 
        [SerializeField] private int grouping;
        public int Grouping => grouping;
        [Range(0,1)]
        [SerializeField] private float groupingVariance;
        public float GroupingVariance => groupingVariance;
        [SerializeField] private int poolingCost;
        public int PoolingCost => poolingCost;
        [SerializeField] private float innerGroupingTimeSeparation;
        public float InnerGroupingTimeSeparation => innerGroupingTimeSeparation;
        [SerializeField] private float outerGroupingTimeSeparation;
        public float OuterGroupingTimeSeparation => outerGroupingTimeSeparation;
        [Range(0, 1)] [SerializeField] private float timeSeparationVariance;
        public float TimeSeparationVariance => timeSeparationVariance;

        [Header("View Data")] 
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        [SerializeField] private float deathAnimationDuration;
        public float DeathAnimationDuration => deathAnimationDuration;
    }
}