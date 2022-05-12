using System;
using UnityEngine;

namespace StressSurvivors
{
    [CreateAssetMenu(fileName = "GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public GamePlayVariables GamePlayVariables;
        public AttackVariables   AttackVariables;
        public SpawnVariables    SpawnVariables;
    }

    [Serializable]
    public class GamePlayVariables
    {
        public float MoveSpeed;
        public int   Health;
    }

    [Serializable]
    public class AttackVariables
    {
        public float SearchRadiusIncrementValue;
        public float SearchRadiusMaxValue;
    }

    [Serializable]
    public class SpawnVariables
    {
        public float SpawnCooldown;
    }
}