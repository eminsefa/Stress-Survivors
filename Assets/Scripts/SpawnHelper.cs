using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StressSurvivors
{
    public static class SpawnHelper
    {
        private static List<Vector2> m_Positions = new();

        private static float MaxY => ScreenManager.CameraHeight / 2f;
        private static float MinX => ScreenManager.CameraWidth  / 2f;

        public static List<Vector2> CalculateHorizontalPositions(Vector3 playerPos, int count, int side)
        {
            var space    = Mathf.CeilToInt(MaxY * 2 / count);
            var bothSide = side == 0;

            m_Positions.Clear();

            var start = Mathf.FloorToInt(-MaxY);
            for (int i = start; i < MaxY; i += space)
            {
                var y = i + Random.Range(-space / 2, space / 2);

                var x = MinX + 1 + Random.Range(-1f, 1f);
                if (bothSide)
                {
                    m_Positions.Add(new Vector2(playerPos.x + x, playerPos.y + y));
                    m_Positions.Add(new Vector2(playerPos.x - x, playerPos.y + y));
                }
                else
                    m_Positions.Add(new Vector2(playerPos.x + x * side, playerPos.y + y));
            }

            return m_Positions;
        }
    }
}