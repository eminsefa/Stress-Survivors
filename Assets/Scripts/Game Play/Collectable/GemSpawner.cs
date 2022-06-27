using UnityEngine;
using Random = UnityEngine.Random;

namespace StressSurvivors
{
    public class GemSpawner : MonoBehaviour
    {
        [SerializeField] private Gem[] m_Gems;

        private GemVariables m_GemVariables => GameConfig.Instance.GemVariables;

        private void OnEnable()
        {
            Enemy.OnEnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            Enemy.OnEnemyDied -= OnEnemyDied;
        }

        private void OnEnemyDied(Enemy enemy)
        {
            var rand  = Random.Range(0, enemy.EnemyData.GemLuck);
            var index = 0;
            for (int i = 0; i < m_Gems.Length; i++)
            {
                if (rand > m_Gems[i].GemData.MinPossibility) index = i;
                else break;
            }

            if (index < 0) return;
            var gem = Instantiate(m_Gems[index], enemy.transform.position, Quaternion.identity).GetComponent<Gem>(); //Change with Pool
            gem.Dropped();
        }
    }
}