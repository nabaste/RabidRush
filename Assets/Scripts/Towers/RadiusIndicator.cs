using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace RabidRush.Towers
{
    public class RadiusIndicator : MonoBehaviour
    {
        [SerializeField] private Material mat;
        [SerializeField] private PlacementManager pm;
        [SerializeField] private TowerModel tm;

        private GameObject _radiusIndicator;
        private float _radius;
        private Vector3 _heightOffset = new Vector3(0, 0.4f, 0);
        
        private void Start()
        {
            _radius = tm.range;
            pm.OnPlacement = () =>
            {
                Destroy(_radiusIndicator);
                Destroy(this);
                
            };
            MakeCircle(50, _radius);
        }

        public void MakeCircle(int numOfPoints, float radius)
        {
            _radiusIndicator = new GameObject
            {
                transform =
                {
                    parent = transform,
                    position = transform.position + _heightOffset,
                },
                name = "Placement Indicator",
                layer = 8
            };

            
            _radiusIndicator.transform.Rotate(-90f, 0, 0);
            
            MeshRenderer meshRenderer = _radiusIndicator.AddComponent<MeshRenderer>();

            MeshFilter meshFilter = _radiusIndicator.AddComponent<MeshFilter>();

            meshRenderer.material = mat;
            
            
            float angleStep = 360.0f / (float)numOfPoints;
            List<Vector3> vertexList = new List<Vector3>();
            List<int> triangleList = new List<int>();
            Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);
            // Make first triangle.
            vertexList.Add(new Vector3(0.0f, 0.0f, 0.0f));  // 1. Circle center.
            vertexList.Add(new Vector3(0.0f, radius, 0.0f));  // 2. First vertex on circle outline 
            vertexList.Add(quaternion * vertexList[1]);     // 3. First vertex on circle outline rotated by angle)
            // Add triangle indices.
            triangleList.Add(0);
            triangleList.Add(1);
            triangleList.Add(2);
            for (int i = 0; i < numOfPoints - 1; i++)
            {
                triangleList.Add(0);                      // Index of circle center.
                triangleList.Add(vertexList.Count - 1);
                triangleList.Add(vertexList.Count);
                vertexList.Add(quaternion * vertexList[vertexList.Count - 1]);
            }
            Mesh mesh = new Mesh();
            mesh.vertices = vertexList.ToArray();
            mesh.triangles = triangleList.ToArray();    
            
            meshFilter.mesh = mesh;
        }
    }
}