using RabidRush.ScriptableObjects;
using UnityEngine;

namespace RabidRush.Towers
{
    public class ChemicalTowerController : TowerController
    {
        [SerializeField] private ChemicalTowerData chemicalTowerData;

        new void Update()
        {
            base.Update();
        }
        protected override void Shoot()
        {
            _enemiesInSight[0].GetDamage(model.damage, kindOfDamage.chemical, chemicalTowerData.Pace, chemicalTowerData.Duration);
            _counter = model.cooldown;
        }
    }
}
