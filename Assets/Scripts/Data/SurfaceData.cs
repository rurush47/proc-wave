using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SurfaceData", menuName = "ProcWave/SurfaceData")]
    public class SurfaceData : ScriptableObject
    {
        public int XSize;
        public int ZSize;
        public Color Color;
    }
}
