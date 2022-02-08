using System;
using UnityEngine;

namespace RabidRush.Towers
{
    public class PlacementManager : MonoBehaviour
    {
        private GameObject _placementChecker;
        private LayerMask _placementLayerMask;
        public Action OnPlacement;

        [SerializeField] private Material mat;
        private Camera _gameCamera;

        private void Awake()
        {
           _placementLayerMask = LayerMask.GetMask("Default");
           _gameCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HandleClick();
            }
            transform.position = FollowCursor();
        }

        private Vector3 FollowCursor()
        {
            var ray = _gameCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit, 400, _placementLayerMask);
            Vector3 mousePos = new Vector3(hit.point.x, 0.2f, hit.point.z);
            return mousePos;
        }

        public void Build(float sizeX, float sizeZ, string tagToCompare)
        {
            _placementChecker = new GameObject
            {
                transform =
                {
                    parent = transform,
                    position = transform.position
                },
                name = "Placement Indicator",
                layer = 8
            };
            var indicator = _placementChecker.AddComponent<PlacementIndicator>();
            indicator.tagToCompare = tagToCompare;

            MeshRenderer meshRenderer = _placementChecker.AddComponent<MeshRenderer>();

            MeshFilter meshFilter = _placementChecker.AddComponent<MeshFilter>();

            meshRenderer.material = mat;

            BoxCollider col = _placementChecker.AddComponent<BoxCollider>();
            col.size = new Vector3(sizeX, 1f, sizeZ);
            col.isTrigger = true;

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4]
            {
                new Vector3(-0.5f * sizeX, 0, -0.5f * sizeZ),
                new Vector3(0.5f * sizeX, 0, -0.5f * sizeZ),
                new Vector3(-0.5f * sizeX, 0, 0.5f * sizeZ),
                new Vector3(0.5f * sizeX, 0, 0.5f * sizeZ)
            };
            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
            mesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
                -Vector3.up,
                -Vector3.up,
                -Vector3.up,
                -Vector3.up
            };
            mesh.normals = normals;

            Vector2[] uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            mesh.uv = uv;

            meshFilter.mesh = mesh;
        }

        private void Destroy()
        {
            Destroy(_placementChecker);
            Destroy(this);
        }

        private void HandleClick()
        {
            if (!_placementChecker.GetComponent<PlacementIndicator>().IsPosValid()) return;
            Destroy();
            OnPlacement?.Invoke();
        }
    }
}