using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    [InlineEditor]
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        public float      MoveSpeed;
        public float      Health;
        public float      WalkAnimationSpeed;
        public EEnemyType EnemyType;

        [Range(0, 100)] public int GemLuck;
    }

    public enum EEnemyType
    {
        One,
        Two,
        Three,
        Four
    }
}