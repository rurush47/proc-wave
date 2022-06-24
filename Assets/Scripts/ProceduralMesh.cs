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
    private Vector3[] _vertices;
    private int[] _triangles;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    private async void GenerateMesh()
    {
        _mesh = new Mesh();
        
        _vertices = new Vector3[(_surfaceData.XSize + 1) * (_surfaceData.ZSize + 1)];
        int index = 0;
        for (int z = 0; z <= _surfaceData.ZSize; z++)
        {
            for (int x = 0; x <= _surfaceData.XSize; x++)
            {
                _vertices[index++] = new Vector3(x, 0, z);
            }
        }

        var colors = new Color[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            colors[i] = _surfaceData.Color;
        }

        _triangles = new int[_surfaceData.XSize * _surfaceData.ZSize * 6];
        index = 0;
        for (int z = 0; z < _surfaceData.ZSize; z++)
        {
            for (int x = 0; x < _surfaceData.XSize; x++)
            {
                _triangles[index++] = x + z + z * _surfaceData.XSize;
                _triangles[index++] = x + z + (z + 1) * _surfaceData.XSize + 1;
                _triangles[index++] = x + z + 1 + z * _surfaceData.XSize;

                await Task.Delay(TimeSpan.FromSeconds(_waitTime));
                
                _triangles[index++] = x + z + 1 + z * _surfaceData.XSize;
                _triangles[index++] = x + z + (z + 1) * _surfaceData.XSize + 1;
                _triangles[index++] = x + z + 1 + (z + 1) * _surfaceData.XSize + 1;
            }
        }
        
        _meshFilter.mesh = _mesh;

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.colors = colors;
        _mesh.RecalculateNormals();
    }

    private void Update()
    {
        for (int z = 0, i = 0; z <= _surfaceData.ZSize; z++)
        {
            for (int x = 0; x <= _surfaceData.XSize; x++, i++)
            {
                float y = 3 * Mathf.Sin(Mathf.PI*((float)x/_surfaceData.XSize + (float)z/_surfaceData.ZSize + Time.realtimeSinceStartup));
                _vertices[i] = new Vector3(x, y, z);
            }
        }
        
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }
}
