using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using RabidRush.ScriptableObjects;

namespace RabidRush.Zombies
{
    public class ZombieModel : MonoBehaviour
    {
        [SerializeField] private ZombieController controller;
        public ZombieData zombieData;
        public LevelData levelData;

        [NonSerialized] public float life;
        [NonSerialized] public float attackCooldown;
        private NavMeshAgent _navMeshAgent = new NavMeshAgent();


        public Action OnDeath;


        private void Start()
        {
            life = zombieData.Life;
            attackCooldown = zombieData.AttackCooldown;

            transform.rotation = Quaternion.LookRotation(levelData.EndPosition - transform.position, transform.up);
            SetUpNavMeshAgent();

            OnDeath += DisableNavMeshAgent;

            controller.OnArrival += HitPlayer;
        }

        private void SetUpNavMeshAgent()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.destination = levelData.EndPosition;
            _navMeshAgent.speed = zombieData.Speed;
        }

        private void DisableNavMeshAgent()
        {
            _navMeshAgent.enabled = false;
        }

        public bool CheckIfDead()
        {
            if (life > 0) return false;
            OnDeath?.Invoke();
            return true;
        }





        private void HitPlayer()
        {
            LevelManager.Instance.LivesLeft--;
        }
    }
}