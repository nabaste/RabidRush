using System;
using System.Collections;
using UnityEngine;

namespace RabidRush.Zombies
{
    public class ZombieController : MonoBehaviour, IDamageable, IComparable, IPoolable
    {
        [SerializeField] private ZombieModel model;
        private Barricade _target;

        public Action OnAttack;
        public Action OnArrival;
        public Action OnDamageTaken;
        public Action OnChemicalDamageTaken;


        private const float DeathAnimationDuration = 0.4f;
        private bool _isChemicallyBurned = false;
        
        public event Action<string, GameObject> OnEliminateObject;
        public string PoolTag { get; set; }

        private void Start()
        {
            OnAttack += DoDamage;
            OnArrival = () => StartCoroutine(DestroyGameObject());
            model.OnDeath = () => StartCoroutine(DestroyGameObject());
        }

        public int CompareTo(object other)
        {
            ZombieController otherZombie = other as ZombieController;
            float otherDistanceToFinish =
                Mathf.Abs((otherZombie.transform.position - model.levelData.EndPosition).magnitude);

            float myDistanceToFinish = Mathf.Abs((transform.position - model.levelData.EndPosition).magnitude);

            int result = (otherDistanceToFinish > myDistanceToFinish) ? -1 : 1;
            return result;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("FinishLine"))
            {
                OnArrival?.Invoke();
            }
        }
        private IEnumerator DestroyGameObject()
        {
            yield return new WaitForSeconds(DeathAnimationDuration);
            OnEliminateObject?.Invoke(PoolTag, gameObject);
            gameObject.SetActive(false);
        }

        #region AttackMechanic

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out Barricade barricade)) return;
            _target = barricade;
            _target.OnDestroyed += CleanTarget;
            OnAttack?.Invoke();
        }

        private void DoDamage()
        {
            _target.GetDamage(model.zombieData.AttackDamage, kindOfDamage.physical);
            if (_target == null) return;
            StartCoroutine(WaitToAttack());
        }

        private IEnumerator WaitToAttack()
        {
            yield return new WaitForSeconds(model.attackCooldown);
            if (_target != null)
            {
                DoDamage();
            }
        }

        private void CleanTarget()
        {
            _target = null;
        }

        #endregion

        #region DamageMechanic

        public void GetDamage(float damage, kindOfDamage kind, float pace = 0, float duration = 0)
        {
            model.life -= damage;
            if (model.CheckIfDead()) return;
            switch (kind)
            {
                case kindOfDamage.physical:
                    OnDamageTaken?.Invoke();
                    break;
                case kindOfDamage.aoe:
                    //something
                    break;
                case kindOfDamage.chemical:
                    GetChemicalDamage(damage, pace, duration);
                    //something
                    break;
            }
        }

        private void GetChemicalDamage(float dmg, float pace, float duration)
        {
            if (!_isChemicallyBurned)
            {
                _isChemicallyBurned = true;
                int numberOfBurns = Mathf.FloorToInt(duration / pace);
                StartCoroutine(ChemicalDamageTimer(numberOfBurns, pace, dmg));
                OnChemicalDamageTaken?.Invoke();
            }
            else
            {
                GetDamage(dmg, kindOfDamage.physical);
            }
        }

        private IEnumerator ChemicalDamageTimer(float rounds, float pace, float dmg)
        {
            WaitForSeconds wait = new WaitForSeconds(pace);
            for (int i = 0; i < rounds; i++)
            {
                GetDamage(dmg, kindOfDamage.chemical);
                yield return wait;
            }

            _isChemicallyBurned = false;
        }

        #endregion
        


    }
}