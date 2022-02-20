using System;
using RabidRush.Towers;
using RabidRush.Zombies;
using UnityEngine;


    public class InspectorCamera : MonoBehaviour
    {
        private bool _watchingSomething = false;
        private Transform _target;
        private Vector3 _offset = new Vector3();
        [SerializeField] private Vector2 offset = new Vector3();
        [SerializeField] private float angleOffset;

        private void LateUpdate()
        {
            if (!_watchingSomething) return;
            _offset = _target.forward;
            _offset *= offset.x;
            _offset.y = offset.y + 0.4f; //average character height
            _offset = Quaternion.AngleAxis(angleOffset, _target.up) * _offset;
            transform.position = _target.position + _offset;
            transform.LookAt(_target);
        }

        public void SetTarget(IInspectable target)
        {
            _target = target.GetTransform();
            _watchingSomething = true;
            if(_target.gameObject.TryGetComponent(out Barricade barr))
            {
                barr.OnDestroyed += StopWatching;
                return;
            }
            if(_target.gameObject.TryGetComponent(out TowerModel tm))
            {
                tm.OnSell += StopWatching;
                return;
            }
            if (!_target.gameObject.TryGetComponent(out ZombieModel zm)) return;
                zm.OnDeath += StopWatching;
        }

        private void StopWatching()
        {
            _target = null;
            _watchingSomething = false;
        }
    }
