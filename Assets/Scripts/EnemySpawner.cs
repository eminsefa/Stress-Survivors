using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        [SerializeField] private PlayerController m_Player;
        [SerializeField] private Enemy[]          m_Enemies;
        [SerializeField] private List<Enemy>      m_EnemyPool;

        [Button]
        private void SetRef()
        {
            m_Enemies   = GetComponentsInChildren<Enemy>();
            m_EnemyPool = GetComponentsInChildren<Enemy>().ToList();
            for (int i = 0; i < m_Enemies.Length; i++)
            {
                m_Enemies[i].SetRef();
            }
            m_Player = FindObjectOfType<PlayerController>();
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied += onEnemyDied;
            for (int i = 0; i < m_Enemies.Length; i++)
            {
                spawnEnemy();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_Enemies.Length; i++)
                m_Enemies[i].OnEnemyDied -= onEnemyDied;
        }

        private void onEnemyDied(Enemy i_Enemy)
        {
            m_EnemyPool.Add(i_Enemy);
            spawnEnemy();
        }

        private void spawnEnemy()
        {
            m_EnemyPool[0].Spawned(Vector2.zero);
            m_EnemyPool.RemoveAt(0);
        }
    }
}