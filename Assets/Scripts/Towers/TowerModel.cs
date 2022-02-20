using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
namespace RabidRush.Towers
{
    public class TowerModel : MonoBehaviour, IInspectable
    {
        [SerializeField] public TowerData towerData;
        private LevelData _levelData;

        [SerializeField] private PlacementManager placementManager;

        [NonSerialized] public float life;
        [NonSerialized] public float range;
        [NonSerialized] public float damage;
        [NonSerialized] public float cooldown;
        [NonSerialized] public float rotationSpeed;
        [NonSerialized] public float angleFOV;
        private List<Vector3> _startLocations = new List<Vector3>();

        public Action OnUpgrade;
        public Action OnSell;

        private void Awake()
        {
            _levelData = LevelManager.Instance.levelData;
            
            life = towerData.Life;
            range = towerData.Range;
            damage = towerData.Damage;
            cooldown = towerData.Cooldown;
            rotationSpeed = towerData.RotationSpeed;
            angleFOV = towerData.AngleFOV;

            placementManager.OnPlacement += PayForTower;
            placementManager.OnPlacement += SetRotationToFaceClosestStartLocation;

            OnSell += SellTower;
        }

        public void Upgrade(Item item)
        {
            switch (item.StatToImprove)
            {
                case TowerStats.Cooldown:
                    cooldown *= item.ChangePercentage;
                    break;
                case TowerStats.Range:
                    range *= item.ChangePercentage;
                    break;
                case TowerStats.Damage:
                    damage *= item.ChangePercentage;
                    break;
            }
            LevelManager.Instance.KartAmount -= towerData.UpgradeCost;
        }

        private void SetRotationToFaceClosestStartLocation()
        {
            foreach (var location in _levelData.StartLocations)
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
            LevelManager.Instance.KartAmount -= towerData.Cost;
        }

        private void SellTower()
        {
            LevelManager.Instance.KartAmount += Mathf.RoundToInt( towerData.Cost * towerData.SellPricePercentage );
            Destroy(gameObject); 
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
            dict.Add("Life", life);
            dict.Add("Attack Damage", towerData.Damage);
            dict.Add("Range", towerData.Range);
            return dict;
        }

        public Dictionary<string, Action> GetPossibleActions()
        {
            var actions = new Dictionary<string, Action>();
            actions.Add($"Upgrade for {towerData.UpgradeCost}", OnUpgrade);
            actions.Add($"Sell for {towerData.Cost * towerData.SellPricePercentage}", OnSell);
            return actions;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        #endregion
    }
}