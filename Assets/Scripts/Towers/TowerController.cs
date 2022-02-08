using System;
using System.Collections.Generic;
using UnityEngine;
using RabidRush.Zombies;

namespace RabidRush.Towers
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] protected TowerModel model;
        [SerializeField] private PlacementManager placementManager;
        protected LayerMask _zombieLayerMask;
        private LayerMask _obstacleLayerMask;

        public Action OnShoot;

        protected List<ZombieController> _enemiesInSight = new List<ZombieController>();
        protected float _counter;

        private void Awake()
        {
            _zombieLayerMask = LayerMask.GetMask("Zombie");
            _obstacleLayerMask = LayerMask.GetMask("Obstacle");

            placementManager.OnPlacement += WakeUp;
            OnShoot = Shoot;

            _counter = model.cooldown;
            
            placementManager.Build(1f, 1f, "Terrain");
        }

        private void WakeUp()
        {
            this.enabled = true;
        }

        protected void Update()
        {
            _counter -= Time.deltaTime;
            _enemiesInSight = MultiTargetLineOfSight();

            if (_counter < 0) OnShoot?.Invoke();
        }

        protected virtual void Shoot()
        {
            _enemiesInSight[0]?.GetDamage(model.damage, kindOfDamage.physical);
            Debug.Log($"Shot {_enemiesInSight[0].name}");
            _counter = model.cooldown;
        }

        private List<ZombieController> MultiTargetLineOfSight()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, model.range, _zombieLayerMask);
            List<ZombieController> result = new List<ZombieController>();
            if (colliders.Length == 0)
            {
                return result;
            }

            Vector3 front = transform.forward;

            foreach (var enemy in colliders)
            {
                Vector3 posDifference = enemy.transform.position - transform.position;
                if (!IsInVisionAngle(posDifference, front)) continue;
                float distance = posDifference.magnitude;
                if (!IsInView(posDifference.normalized, model.range, _obstacleLayerMask)) continue;
                result.Add(enemy.gameObject.GetComponent<ZombieController>());
            }
            ConsiderPrioritaryTarget(result);
            model.LookAtTarget(result[0].transform);
            return result;
        }

        private bool IsInVisionAngle(Vector3 origin, Vector3 target)
        {
            float angleToTarget = Vector3.Angle(origin, target);
            return angleToTarget < (model.angleFOV / 2);
        }

        private bool IsInView(Vector3 normalizedDirection, float distance, LayerMask obstacleMask)
        {
            bool result;
            result = !Physics.Raycast(transform.position, normalizedDirection, distance, obstacleMask);
            return result;
        }

        private void ConsiderPrioritaryTarget(List<ZombieController> enemies)
        {
            enemies.Sort();
        }
    }
}