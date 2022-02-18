using System;
using UnityEngine;


    public class InspectorCamera : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(1f, 0.3f, 0.6f);

        private void Update()
        {
            transform.position = _target.position + Vector3.Cross( _target.forward, _offset);
            transform.LookAt(_target);
        }

        public void SetTarget(IInspectable target)
        {
            _target = target.GetTransform();
        }
    }
