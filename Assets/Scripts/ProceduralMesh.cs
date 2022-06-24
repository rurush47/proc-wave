using System;
using System.Threading.Tasks;
using Data;
using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
    [SerializeField] private MeshData meshData;
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
        
        _vertices = new Vector3[(meshData.XSize + 1) * (meshData.ZSize + 1)];
        int index = 0;
        for (int z = 0; z <= meshData.ZSize; z++)
        {
            for (int x = 0; x <= meshData.XSize; x++)
            {
                _vertices[index++] = new Vector3(x, 0, z);
            }
        }

        var colors = new Color[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            colors[i] = meshData.Color;
        }

        _triangles = new int[meshData.XSize * meshData.ZSize * 6];
        index = 0;
        for (int z = 0; z < meshData.ZSize; z++)
        {
            for (int x = 0; x < meshData.XSize; x++)
            {
                _triangles[index++] = x + z + z * meshData.XSize;
                _triangles[index++] = x + z + (z + 1) * meshData.XSize + 1;
                _triangles[index++] = x + z + 1 + z * meshData.XSize;

                await Task.Delay(TimeSpan.FromSeconds(_waitTime));
                
                _triangles[index++] = x + z + 1 + z * meshData.XSize;
                _triangles[index++] = x + z + (z + 1) * meshData.XSize + 1;
                _triangles[index++] = x + z + 1 + (z + 1) * meshData.XSize + 1;
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
        for (int z = 0, i = 0; z <= meshData.ZSize; z++)
        {
            for (int x = 0; x <= meshData.XSize; x++, i++)
            {
                float y = 3 * Mathf.Sin(Mathf.PI*((float)x/meshData.XSize + (float)z/meshData.ZSize + Time.realtimeSinceStartup));
                _vertices[i] = new Vector3(x, y, z);
            }
        }
        
        _mesh.vertices = _vertices;
        _mesh.RecalculateNormals();
    }
}
