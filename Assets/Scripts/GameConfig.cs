using System;
using UnityEngine;

namespace StressSurvivors
{
    [CreateAssetMenu(fileName = "GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public PlayerVariables PlayerVariables;
        public AttackVariables   AttackVariables;
    }

    [Serializable]
    public class PlayerVariables
    {
        public float MoveSpeed;
        public int   Health;
    }

    [Serializable]
    public class AttackVariables
    {
        public SearchClosest SearchClosest;
    }
    [Serializable]
    public class EnemyTypes
    {
        public SearchClosest SearchClosest;
    }

    [Serializable]
    public class SearchClosest
    {
        public float SearchRadiusIncrementValue;
    }

    [Serializable]
    public class SpawnData
    {
        public SpawnType[] SpawnType;
    }
    [Serializable]
    public class SpawnType
    {
        public float      Cooldown;
        public int        SpawnCount;
        public ESpawnType SpawnTypeEnum;
    }

    public enum ESpawnType
    {
        Horizontal,
        DoubleHorizontal
    }
}