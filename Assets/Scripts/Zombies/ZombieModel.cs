using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RabidRush.ScriptableObjects;

namespace RabidRush.Zombies
{
    public class ZombieModel : MonoBehaviour, IInspectable
    {
        [SerializeField] private ZombieController controller;
        public ZombieData zombieData;
        public LevelData levelData;

        [NonSerialized] public float life;
        [NonSerialized] public float attackCooldown;
        private NavMeshAgent _navMeshAgent = new NavMeshAgent();

        public Action OnDeath;
        public Action OnPrioritized;
        

        private void Start()
        {
            levelData = LevelManager.Instance.levelData;
            
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

        public IEnumerator StopNavMesh(float time)
        {
            _navMeshAgent.speed = 0;
            yield return new WaitForSeconds(time);
            _navMeshAgent.speed = zombieData.Speed;
        }

        public bool CheckIfDead()
        {
            if (life > 0) return false;
            OnDeath?.Invoke();
            return true;
        }



        #region IInspectable
        private void HitPlayer()
        {
            LevelManager.Instance.livesLeft--;
        }

        public string GetName()
        {
            return zombieData.name;
        }

        public float GetLife()
        {
            return life;
        }

        public Dictionary<string, float> GetStats()
        {
            var dict = new Dictionary<string, float>();
            dict.Add("Life", life);
            dict.Add("Attack Damage", zombieData.AttackDamage);
            return dict;
        }
        
        public Dictionary<string, Action> GetPossibleActions()
        {
            var actions = new Dictionary<string, Action>();
            actions.Add("Make Priority", OnPrioritized);
            return actions;
        }

        public Transform GetTransform()
        {
            return transform;
        }
        #endregion
    }
}