using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.Towers
{
    public class PlacementIndicator : MonoBehaviour
    {
        private readonly Color _red = new Color(255f, 0f, 0f, 0.6f);
        private readonly Color _green = new Color(0f, 255f, 0f, 0.6f);
        private Material _mat;
    
        private readonly List<GameObject> _collidingObjects = new List<GameObject>();
        public string tagToCompare;
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.isKinematic = false;
            _rb.useGravity = false;
        }

        private void Start()
        {
            _mat = GetComponent<MeshRenderer>().material;
        }


        private void OnTriggerEnter(Collider col)
        {
            _collidingObjects.Add(col.gameObject);
            IsPosValid();
        }
        private void OnTriggerExit(Collider col)
        {
            _collidingObjects.Remove(col.gameObject);
            IsPosValid();
        }
    
        public bool IsPosValid()
        {
            var isValid = false;
            foreach (var col in _collidingObjects)
            {
                if (col.CompareTag(tagToCompare))
                {
                    isValid = true;
                }
                else
                {
                    _mat.color = _red;
                    return false;
                }
            }
            if (_collidingObjects.Count > 0)
            {
                _mat.color = _green;
            }
            return isValid;
        }
        
    }
}