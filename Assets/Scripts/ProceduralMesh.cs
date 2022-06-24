using System;
using System.Threading.Tasks;
using Data;
using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
    [SerializeField] private SurfaceData _surfaceData;
    [SerializeField] private float _waitTime;
    private MeshFilter _meshFilter;
    private Mesh _mesh;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    private Vector3[] vertices;
    private int[] triangles;
    
    private async void GenerateMesh()
    {
        _mesh = new Mesh();
        
        vertices = new Vector3[(_surfaceData.XSize + 1) * (_surfaceData.ZSize + 1)];
        int index = 0;
        for (int z = 0; z <= _surfaceData.ZSize; z++)
        {
            for (int x = 0; x <= _surfaceData.XSize; x++)
            {
                vertices[index++] = new Vector3(x, 0, z);
            }
        }

        triangles = new int[_surfaceData.XSize * _surfaceData.ZSize * 6];
        index = 0;
        for (int z = 0; z < _surfaceData.ZSize; z++)
        {
            for (int x = 0; x < _surfaceData.XSize; x++)
            {
                triangles[index++] = x + z + z * _surfaceData.XSize;
                triangles[index++] = x + z + (z + 1) * _surfaceData.XSize + 1;
                triangles[index++] = x + z + 1 + z * _surfaceData.XSize;

                await Task.Delay(TimeSpan.FromSeconds(_waitTime));
                
                triangles[index++] = x + z + 1 + z * _surfaceData.XSize;
                triangles[index++] = x + z + (z + 1) * _surfaceData.XSize + 1;
                triangles[index++] = x + z + 1 + (z + 1) * _surfaceData.XSize + 1;
            }
        }
        
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
        _meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
        _meshFilter.mesh = _mesh;
    }
}
