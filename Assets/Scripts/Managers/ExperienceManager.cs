using System;
using DG.Tweening;

namespace StressSurvivors
{
    public class ExperienceManager : Singleton<ExperienceManager>
    {
        public static event Action OnLevelUp;

        private int m_Experience;
        private int m_CurrentLevel;

        private ExperienceVariables m_ExperienceVariables => GameConfig.Instance.PlayerVariables.ExperienceVariables;

        private void OnEnable()
        {
            Gem.OnGemCollected += OnGemCollected;
        }

        private void OnDisable()
        {
            Gem.OnGemCollected -= OnGemCollected;
        }

        private void OnGemCollected(GemData gemData)
        {
            var value = gemData.ExpValue;
            DOTween.To(() => m_Experience, x => m_Experience = x, value, value * m_ExperienceVariables.GainExperienceDuration)
                   .SetRelative(true)
                   .OnUpdate(CheckForLevelUp);
        }

        private void CheckForLevelUp()
        {
            return;
            var nextLevelLimit = m_ExperienceVariables.ExperienceLevels[m_CurrentLevel];
            if (m_Experience >= nextLevelLimit)
            {
                m_Experience -= nextLevelLimit;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            m_CurrentLevel += 1;
            OnLevelUp?.Invoke();
        }
    }
}