using System;
using System.Collections.Generic;
using StressSurvivors;
using UnityEngine;

namespace StressSurvivors
{
    [CreateAssetMenu(fileName = "GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public PlayerVariables PlayerVariables;
        public AttackVariables AttackVariables;
        public GemVariables    GemVariables;
        public EnemyVariables  EnemyVariables;
    }

    [Serializable]
    public class PlayerVariables
    {
        public float MoveSpeed;
        public float MoveThreshold;
        public int   Health;
        public float MinWalkAnimationSpeed;
        public float MaxWalkAnimationSpeed;

        public ExperienceVariables ExperienceVariables;
    }

    [Serializable]
    public class ExperienceVariables
    {
        public float GainExperienceDuration;
        public int[] ExperienceLevels;
    }

    [Serializable]
    public class AttackVariables
    {
        public SearchClosest SearchClosest;
    }

    [Serializable]
    public class SearchClosest
    {
        public float SearchRadiusIncrementValue;
    }

    [Serializable]
    public class GemVariables
    {
        public float     DropJumpValue;
        public float     DropJumpDuration;
        public GemData[] GemDatas;

        //public GemPossibilityDictionary GemPossibilityDictionary;
    }

    [Serializable]
    public class EnemyVariables
    {
        public EnemyData[] EnemyDatas;
    }
}

[Serializable] public class GemPossibilityDictionary : UnitySerializedDictionary<float, GemData> { }