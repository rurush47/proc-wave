using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MeshData", menuName = "ProcWave/MeshData")]
    public class MeshData : ScriptableObject
    {
        public int XSize;
        public int ZSize;
        public Color Color;
        public Material WireframeMaterial;
        public Material FallbackWireframeMaterial;
        public Material DiffuseMaterial;

        private void OnEnable()
        {
            #if UNITY_STANDALONE_OSX
                WireframeMaterial = FallbackWireframeMaterial;
            #endif
        }
    }
}