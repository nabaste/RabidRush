using UnityEngine;
using RabidRush.Zombies;

namespace RabidRush.Towers
{
    public class AoETowerController : TowerController
    {
        [SerializeField] private float _impactRange;
        new void Update()
        {
            base.Update();
        }
        
        protected override void Shoot()
        {
            ZombieController zombieTarget = _enemiesInSight[0];
            Vector3 impactCenter = zombieTarget.gameObject.transform.position;
            Collider[] colliders = Physics.OverlapSphere(impactCenter, _impactRange, _zombieLayerMask);
            Vector3 maxReach = new Vector3(impactCenter.x + _impactRange + 3, impactCenter.y, impactCenter.z); //extra range for objects on the border.
            float maxDistance = (impactCenter - maxReach).magnitude;
            for (int i = 0; i < colliders.Length; i++)
            {
                float distanceToHit = (impactCenter - colliders[i].gameObject.transform.position).magnitude;
                float intensity = 1 - distanceToHit / maxDistance;
                colliders[i].gameObject.GetComponent<ZombieController>().GetDamage(intensity * model.damage, kindOfDamage.aoe);
            }
            _counter = model.cooldown;
        }
    }
}
