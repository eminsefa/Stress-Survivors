using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

namespace StressSurvivors
{
    public class PlayerController : Singleton<PlayerController>
    {
        private PlayerVariables PlayerVariables => GameConfig.Instance.PlayerVariables;

        private int     m_Health;
        private Vector2 m_Input;

        [SerializeField] private FloatingJoystick  m_Joystick;
        [SerializeField] private Rigidbody2D       m_Rigidbody;
        [SerializeField] private SkeletonAnimation m_SkeletonAnimation;

        [Button]
        private void SetRef()
        {
            m_Joystick          = FindObjectOfType<FloatingJoystick>();
            m_Rigidbody         = GetComponent<Rigidbody2D>();
            m_SkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
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

            var magnitude = m_Input.magnitude;

            if (magnitude < PlayerVariables.MoveThreshold)
            {
                m_SkeletonAnimation.timeScale = 0;
                return;
            }

            m_Rigidbody.MovePosition(pos);

            var rot = m_Input.x < 0 ? 180f : 0;
            m_SkeletonAnimation.transform.localRotation = Quaternion.Euler(0, rot, 0);

            m_SkeletonAnimation.timeScale = Mathf.Lerp(PlayerVariables.MinWalkAnimationSpeed,
                                                       PlayerVariables.MaxWalkAnimationSpeed,
                                                       Mathf.InverseLerp(0, 1, magnitude));
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