using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig")]
public class GameConfig : SingletonScriptableObject<GameConfig>
{
    public GamePlayVariables m_GamePlayVariables;
    public AttackVariables m_AttackVariables;
    
}
[Serializable]
public class GamePlayVariables
{
    public float MoveSpeed;
    public int Health;
}

[Serializable]
public class AttackVariables
{
    public float        SearchRadiusIncrementValue;
    public float        SearchRadiusMaxValue;
}