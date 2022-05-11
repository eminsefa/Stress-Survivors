using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class Enemy : EnemyBase
    {
        public event Action<Enemy> OnEnemyDied;

        private Vector3 m_MoveDir;

        [SerializeField] private Transform   m_Player;
        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private Collider2D  m_Collider;
        [SerializeField] private Transform   m_Body;

        [Button]
        public void SetRef()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Body      = transform.Find("Body");
            m_Collider  = m_Body.GetComponent<Collider2D>();
            m_Player    = FindObjectOfType<PlayerController>().transform;
        }

        public void Spawned(Vector3 i_Position)
        {
            transform.position = i_Position;
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
                m_Rigidbody.AddForce(m_MoveDir * MoveSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D i_Col)
        {
            Health--;
            if (Health < 0)
                dead();
        }

        private void dead()
        {
            m_Body.gameObject.SetActive(false);
            IsAlive            = false;
            m_Collider.enabled = false;
            OnEnemyDied?.Invoke(this);
        }
    }
}