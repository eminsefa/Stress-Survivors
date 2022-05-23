using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class PlayerController : Singleton<PlayerController>
    {
        private PlayerVariables PlayerVariables => GameConfig.Instance.PlayerVariables;

        private int     m_Health;
        private Vector2 m_Input;

        [SerializeField] private FloatingJoystick m_Joystick;
        [SerializeField] private Rigidbody2D      m_Rigidbody;
        [SerializeField] private Gun              m_Gun;

        [Button]
        private void SetRef()
        {
            m_Joystick  = FindObjectOfType<FloatingJoystick>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_Health = PlayerVariables.Health;

            GameManager.OnGameReset += OnReset;
        }

        private void OnDisable()
        {
            GameManager.OnGameReset -= OnReset;
        }

        private void Update()
        {
            m_Input = new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical);
        }

        private void FixedUpdate()
        {
            var pos = (Vector2) m_Rigidbody.position;
            pos += m_Input * PlayerVariables.MoveSpeed;
            m_Rigidbody.MovePosition(pos);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            m_Health--;
            if (m_Health <= 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
            GameManager.Instance.LevelFailed();
        }

        private void OnReset()
        {
            m_Health = PlayerVariables.Health;
        }
    }
}