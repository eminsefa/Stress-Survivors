using System;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace StressSurvivors
{
    public class Enemy : EnemyBase
    {
        public event Action<Enemy> OnEnemyDied;

        
        private Vector3 m_MoveDir;

        [SerializeField] private EnemyData         m_EnemyData;
        [SerializeField] private Transform         m_Player;
        [SerializeField] private Rigidbody2D       m_Rigidbody;
        [SerializeField] private Collider2D        m_Collider;
        [SerializeField] private Transform         m_Body;
        [SerializeField] private SkeletonAnimation m_SkeletonAnimation;

        [Button]
        public void SetRefs()
        {
            m_Rigidbody         = GetComponent<Rigidbody2D>();
            m_Body              = transform.Find("Body");
            m_Collider          = m_Body.GetComponent<Collider2D>();
            m_Player            = FindObjectOfType<PlayerController>().transform;
            m_SkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        }

        private void OnEnable()
        {
            m_SkeletonAnimation.timeScale = m_EnemyData.WalkAnimationSpeed;
        }

        public void Spawned(Vector3 position)
        {
            transform.position = position;
            m_Body.gameObject.SetActive(true);
            IsAlive            = true;
            m_Collider.enabled = true;
        }

        private void Update()
        {
            if (IsAlive)
            {
                m_MoveDir = ((Vector2) m_Player.position - m_Rigidbody.position).normalized;
            }
        }

        private void FixedUpdate()
        {
            if (IsAlive)
            {
                var pos = transform.position;
                pos += m_MoveDir*m_EnemyData.MoveSpeed;
                m_Rigidbody.MovePosition(pos);
                //m_Rigidbody.AddForce(m_MoveDir * m_EnemyData.MoveSpeed);
                
                var rot = m_Rigidbody.velocity.x < 0 ? 180f : 0;
                m_SkeletonAnimation.transform.localRotation = Quaternion.Euler(0, rot, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Health--;
            if (Health < 0)
                Dead();
        }

        private void Dead()
        {
            m_Body.gameObject.SetActive(false);
            IsAlive            = false;
            m_Collider.enabled = false;
            OnEnemyDied?.Invoke(this);
        }
    }
}