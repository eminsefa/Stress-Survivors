using UnityEngine;

namespace StressSurvivors
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        public float MoveSpeed;
        public float Health;
        public float WalkAnimationSpeed;
    }
}