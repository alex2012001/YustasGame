using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public float Speed;
        public float RotationSpeed;
    }
}
