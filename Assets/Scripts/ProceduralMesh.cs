using System;
using System.Threading.Tasks;
using Data;
using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
    [SerializeField] private MeshData _meshData;
    [SerializeField] private KeyCode _switchMaterialKey;
    [SerializeField] private float _waitTime;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    private async void GenerateMesh()
    {
        _mesh = new Mesh();
        
        _vertices = new Vector3[(_meshData.XSize + 1) * (_meshData.ZSize + 1)];
        CalculateVertices(ref _vertices);

        var colors = new Color[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            colors[i] = _meshData.Color;
        }

        _triangles = new int[_meshData.XSize * _meshData.ZSize * 6];
        for (int z = 0, i = 0; z < _meshData.ZSize; z++)
        {
            for (int x = 0; x < _meshData.XSize; x++)
            {
                _triangles[i++] = x + z + z * _meshData.XSize;
                _triangles[i++] = x + z + (z + 1) * _meshData.XSize + 1;
                _triangles[i++] = x + z + 1 + z * _meshData.XSize;

                await Task.Delay(TimeSpan.FromSeconds(_waitTime));
                
                _triangles[i++] = x + z + 1 + z * _meshData.XSize;
                _triangles[i++] = x + z + (z + 1) * _meshData.XSize + 1;
                _triangles[i++] = x + z + 1 + (z + 1) * _meshData.XSize + 1;
            }
        }
        
        _meshFilter.mesh = _mesh;
        _mesh.vertices = _vertices;
        _mesh.colors = colors;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
    }

    private void CalculateVertices(ref Vector3[] vertices)
    {
        for (int z = 0, i = 0; z <= _meshData.ZSize; z++)
        {
            for (int x = 0; x <= _meshData.XSize; x++, i++)
            {
                float y = 3 * Mathf.Sin(Mathf.PI*((float)x/_meshData.XSize + (float)z/_meshData.ZSize + Time.realtimeSinceStartup));
                vertices[i] = new Vector3(x, y, z);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(_switchMaterialKey))
        {
            _meshRenderer.material =
                _meshRenderer.material.name.Contains(_meshData.WireframeMaterial.name)
                    ? _meshData.DiffuseMaterial
                    : _meshData.WireframeMaterial;
        }
        
        CalculateVertices(ref _vertices);
        
        _mesh.vertices = _vertices;
        _mesh.RecalculateNormals();
    }
}
