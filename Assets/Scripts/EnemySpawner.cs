using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StressSurvivors
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        private float m_SpawnTimer;
        private float m_CameraWidth;
        private int   m_SpawnNumber;
        private bool  m_Spawning = true;

        private static List<Vector2> s_SpawnPositions = new List<Vector2>();

        [SerializeField] private PlayerController m_Player;
        [SerializeField] private SpawnData        m_SpawnData;
        [SerializeField] private Camera           m_MainCam;
        [SerializeField] private Enemy[]          m_Enemies;
        [SerializeField] private List<Enemy>      m_EnemyPool;

        [Button]
        private void SetRefs()
        {
            m_Enemies   = GetComponentsInChildren<Enemy>();
            m_EnemyPool = GetComponentsInChildren<Enemy>().ToList();
            for (int i = 0; i < m_Enemies.Length; i++)
            {
                m_Enemies[i].SetRefs();
            }

            m_Player  = FindObjectOfType<PlayerController>();
            m_MainCam = Camera.main;
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied -= OnEnemyDied;
        }

        private void FixedUpdate()
        {
            if (!m_Spawning) return;
            m_SpawnTimer += Time.fixedDeltaTime;
            if (m_SpawnTimer > m_SpawnData.SpawnType[m_SpawnNumber].Cooldown)
            {
                m_SpawnTimer = 0;
                DecideSpawn();
            }
        }

        private void DecideSpawn()
        {
            var type  = m_SpawnData.SpawnType[m_SpawnNumber];
            var count = m_SpawnData.SpawnType[m_SpawnNumber].SpawnCount;

            if (m_SpawnNumber + 1 > m_SpawnData.SpawnType.Length) m_Spawning = false;
            m_SpawnNumber++;
            
            s_SpawnPositions.Clear();
            switch (type.SpawnTypeEnum)
            {
                case ESpawnType.Horizontal:
                    var side = Random.Range(0, 2) * 2 - 1;
                    s_SpawnPositions = SpawnHelper.CalculateHorizontalPositions(m_Player.transform.position, count, side);
                    break;
                case ESpawnType.DoubleHorizontal:
                    s_SpawnPositions = SpawnHelper.CalculateHorizontalPositions(m_Player.transform.position, count, 0);
                    break;
            }

            for (int i = 0; i < s_SpawnPositions.Count; i++)
            {
                SpawnEnemy(s_SpawnPositions[i]);
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            m_EnemyPool.Add(enemy);
        }

        private void SpawnEnemy(Vector2 spawnPos)
        {
            m_EnemyPool[0].Spawned(spawnPos);
            m_EnemyPool.RemoveAt(0);
        }
    }
}