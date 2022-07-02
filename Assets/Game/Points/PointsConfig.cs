using UnityEngine;

namespace Game.Points
{
    [CreateAssetMenu (fileName = "PointsConfig", menuName = "Config/PointsConfig")]
    public class PointsConfig : ScriptableObject
    {
        public Point Prefab;
        
        public float MaxWeight;
        public float MinWeight;
        public float MaxHeight;
        public float MinHeight;
        
        public int MaxNumberOfPoints;
    }
}
