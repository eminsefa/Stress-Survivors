using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        private float m_SpawnTimer;
        private float m_CameraWidth;
        private float m_CameraHeight;

        private SpawnVariables m_SpawnVariables => GameConfig.Instance.SpawnVariables;
        
        [SerializeField] private PlayerController m_Player;
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

            m_Player = FindObjectOfType<PlayerController>();
            m_MainCam = Camera.main;
        }

        private void Awake()
        {
            m_CameraHeight = m_MainCam.orthographicSize * 2;
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied += OnEnemyDied;
            for (int i = 0; i < m_Enemies.Length; i++)
            {
                SpawnEnemy();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied -= OnEnemyDied;
        }

        private void FixedUpdate()
        {
            m_SpawnTimer += Time.fixedDeltaTime;
            if (m_SpawnTimer > m_SpawnVariables.SpawnCooldown)
            {
                m_SpawnTimer = 0;
                DecideSpawn();
            }
        }

        private void DecideSpawn()
        {
            //var positions = SpawnHelper.SpawnHorizontal(m_Player,m_CameraHeight, 5);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            m_EnemyPool.Add(enemy);
        }

        private void SpawnEnemy()
        {
            m_EnemyPool[0].Spawned(Vector2.zero);
            m_EnemyPool.RemoveAt(0);
        }
    }
}