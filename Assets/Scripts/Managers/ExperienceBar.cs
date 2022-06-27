using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace StressSurvivors
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] private Image m_ExperienceFill;

        [Button]
        private void SetRefs()
        {
            m_ExperienceFill = GameObject.Find("Experience Fill").GetComponent<Image>();
        }

        private void OnEnable()
        {
            ExperienceManager.OnExperienceGained += UpdateExperienceBar;
            ExperienceManager.OnLevelUp          += OnLevelUp;
        }

        private void OnDisable()
        {
            ExperienceManager.OnExperienceGained -= UpdateExperienceBar;
            ExperienceManager.OnLevelUp          -= OnLevelUp;
        }

        private void UpdateExperienceBar(float percent)
        {
            m_ExperienceFill.fillAmount = percent;
        }

        private void OnLevelUp()
        {
            
        }
    }
}