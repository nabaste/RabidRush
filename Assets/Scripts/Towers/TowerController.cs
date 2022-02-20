using System;
using System.Collections.Generic;
using UnityEngine;
using RabidRush.Zombies;
using UnityEngine.SocialPlatforms;

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
            OnShoot += Shoot;

            _counter = model.cooldown;

            placementManager.Build(model.towerData.PlacerSizeX, model.towerData.PlacerSizeZ, "Terrain");
        }

        public void WakeUp()
        {
            enabled = true;
        }

        protected void Update()
        {
            _counter -= Time.deltaTime;
            TargetEnemy();
            _enemiesInSight = MultiTargetLineOfSight();

            if (_counter <= 0 && _enemiesInSight.Count > 0) OnShoot?.Invoke();
        }

        protected virtual void Shoot()
        {
            _enemiesInSight[0]?.GetDamage(model.damage, kindOfDamage.physical);
            _counter = model.cooldown;
        }
        
        protected void TargetEnemy()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, model.range, _zombieLayerMask);
            List<ZombieController> results = new List<ZombieController>();
            if (colliders.Length == 0) return;
            for (int i = 0; i < colliders.Length; i++)
            {
                results.Add(colliders[i].gameObject.GetComponent<ZombieController>());
            }
            ConsiderPrioritaryTarget(results);
            LookAtTarget(results[0].transform);
        }
        void LookAtTarget(Transform target)
        {
            Vector3 targetDirection = new Vector3(target.position.x, target.position.y, target.position.z);
            Quaternion newRotation = Quaternion.LookRotation(targetDirection - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, model.towerData.RotationSpeed * Time.deltaTime);
        }

        private List<ZombieController> MultiTargetLineOfSight()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, model.range, _zombieLayerMask);
            List<ZombieController> result = new List<ZombieController>();
            if (colliders.Length == 0) return result;


            Vector3 front = transform.forward;
            
            for (int i = 0; i < colliders.Length; i++)
            {
                var current = colliders[i];
                Vector3 posDifference = current.transform.position - transform.position;
                if (!IsInVisionAngle(posDifference, front)) continue;
                float distance = posDifference.magnitude;
                if (!IsInView(posDifference.normalized, model.range, _obstacleLayerMask)) continue;
                // if (!current.gameObject.TryGetComponent(out ZombieController zc)) return new List<ZombieController>();
                // if(current.gameObject.GetComponent<ZombieController>())
                result.Add(current.gameObject.GetComponent<ZombieController>());
            }


            // ConsiderPrioritaryTarget(result);
            // model.LookAtTarget(result[0].transform);
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