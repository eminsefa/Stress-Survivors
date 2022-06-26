using UnityEngine;
using Random = UnityEngine.Random;

namespace StressSurvivors
{
    public class Gun : AttackBase
    {
        private float   m_Timer;
        private float   m_SearchRadius;
        private Vector2 m_AttackPosition;

        private AttackVariables m_AttackVariables => GameConfig.Instance.AttackVariables;

        [SerializeField] private Rigidbody2D      m_Rigidbody;
        [SerializeField] private PlayerController m_PlayerController;
        [SerializeField] private LayerMask        m_EnemyLayer;

        private void OnEnable()
        {
            IsActive       = true;
            Attack();
        }
        

        private void FixedUpdate()
        {
            if (IsActive)
            {
                m_Timer += Time.fixedDeltaTime;
                if (m_Timer > Cooldown - 1)
                {
                    m_AttackPosition = FindClosestEnemyPosition();
                    if (m_Timer > Cooldown)
                    {
                        Attack();
                    }
                }
            }
        }
        private void Attack()
        {
            IsActive             = false;
            m_Rigidbody.velocity = Vector2.zero;

            if (m_AttackPosition == Vector2.zero)
            {
                m_AttackPosition = FindClosestEnemyPosition();
                if (m_AttackPosition == Vector2.zero)
                {
                    var random =new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                    m_AttackPosition = (Vector2) m_PlayerController.transform.position + random;
                }
            }

            var playerPos = (Vector2) m_PlayerController.transform.position;
            var dir       = (m_AttackPosition - playerPos).normalized;
            transform.position = playerPos;
            m_Rigidbody.AddForce(dir * FireSpeed);

            m_SearchRadius         = 0;
            IsActive               = true;
            m_Timer                = 0;
            m_AttackPosition = Vector2.zero;
            base.Attack();
        }
        
        private Vector2 FindClosestEnemyPosition()
        {       
            var hits = new Collider2D[16];

            var hitCount = Physics2D.OverlapCircleNonAlloc(m_PlayerController.transform.position, m_SearchRadius, hits, m_EnemyLayer);
            if (hitCount == 0)
            {
                m_SearchRadius += m_AttackVariables.SearchClosest.SearchRadiusIncrementValue * Time.fixedDeltaTime;
                if (m_SearchRadius > ScreenManager.ScreenRadius)
                {
                    return Vector2.zero;
                }

                return FindClosestEnemyPosition();
            }

            if (hitCount == 1) return hits[0].transform.position;

            var closestDist = float.MaxValue;
            var closest     = hits[0].transform;
            for (int i = 0; i < hitCount; i++)
            {
                var dist = Vector2.Distance(hits[i].transform.position, m_PlayerController.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest     = hits[i].transform;
                }
            }

            return closest.position;
        }
    }
}