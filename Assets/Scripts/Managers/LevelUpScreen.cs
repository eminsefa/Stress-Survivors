using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    public class LevelUpScreen : MonoBehaviour
    {
        public static event Action OnBuffSelected;

        [SerializeField] private GameObject m_LevelUpBackground;

        [Button]
        private void SetRefs()
        {
            m_LevelUpBackground = GameObject.Find("Level Up Background");
        }

        private void OnEnable()
        {
            ExperienceManager.OnLevelUp += OnLevelUp;
        }

        private void OnDisable()
        {
            ExperienceManager.OnLevelUp -= OnLevelUp;
        }

        private void OnLevelUp()
        {
            m_LevelUpBackground.SetActive(true);
        }

        public void OnSelected()
        {
            m_LevelUpBackground.SetActive(false);
            OnBuffSelected?.Invoke();
        }
    }
}