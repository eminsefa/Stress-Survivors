using System.Collections.Generic;
using UnityEngine;

namespace StressSurvivors
{
    public class SpawnHelper : Singleton<SpawnHelper>
    {
        //***ON CONSTRUCTION***//
        
        // private static List<Vector2> m_Positions = new();
        
        // public static List<Vector2> SpawnHorizontal(PlayerController player, float height, int count)
        // {
        //     var space = Mathf.FloorToInt(height / count);
        //
        //     m_Positions.Clear();
        //
        //     var start = Mathf.FloorToInt(-height / 2f);
        //     for (int i = start; i < height; i += space)
        //     {
        //         var y = i + Random.Range(-space / 2, space / 2);
        //         m_Positions.Add(y);
        //     }
        //
        //     return m_Positions;
        // }
    }
}