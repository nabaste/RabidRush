using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;

namespace RabidRush.Towers
{
    public class TowerModel : MonoBehaviour, IInspectable
    {
        [SerializeField] public TowerData towerData;
        [SerializeField] private LevelData levelData;
        [SerializeField] private PlayerData playerData;

        [SerializeField] private PlacementManager placementManager;

        [NonSerialized] public float life;
        [NonSerialized] public float range;
        [NonSerialized] public float damage;
        [NonSerialized] public float cooldown;
        [NonSerialized] public float rotationSpeed;
        [NonSerialized] public float angleFOV;
        private List<Vector3> _startLocations = new List<Vector3>();

        public Action OnUpgrade;

        private void Awake()
        {
            life = towerData.Life;
            range = towerData.Range;
            damage = towerData.Damage;
            cooldown = towerData.Cooldown;
            rotationSpeed = towerData.RotationSpeed;
            angleFOV = towerData.AngleFOV;

            placementManager.OnPlacement += PayForTower;
            placementManager.OnPlacement += SetRotationToFaceClosestStartLocation;
        }

        public void Upgrade(TowerStats stat, float amount)
        {
            switch (stat)
            {
                case TowerStats.Cooldown:
                    cooldown *= amount;
                    break;
                case TowerStats.Range:
                    range *= amount;
                    break;
                case TowerStats.Damage:
                    damage *= amount;
                    break;
            }

            OnUpgrade?.Invoke();
        }

        private void SetRotationToFaceClosestStartLocation()
        {
            foreach (var location in levelData.StartLocations)
            {
                _startLocations.Add(location.Position - transform.position);
            }

            _startLocations.Sort(new Vector3Comparer());
            transform.rotation = Quaternion.LookRotation(_startLocations[0], transform.up);
        }

        public void LookAtTarget(Transform target)
        {
            var targetPosition = target.position;
            Vector3 targetDirection = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            Quaternion newRotation = Quaternion.LookRotation(targetDirection - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        private void PayForTower()
        {
            playerData.KartAmount -= towerData.Cost;
        }

        #region IInspectable

        public string GetName()
        {
            return towerData.TowerName;
        }

        public float GetLife()
        {
            return life;
        }

        public Dictionary<string, float> GetStats()
        {
            var dict = new Dictionary<string, float>();
            //...
            return dict;
        }

        #endregion
    }
}