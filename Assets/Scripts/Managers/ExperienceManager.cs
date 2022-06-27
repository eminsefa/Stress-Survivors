using System;
using DG.Tweening;

namespace StressSurvivors
{
    public class ExperienceManager : Singleton<ExperienceManager>
    {
        public static event Action OnLevelUp;
        public static event Action<float> OnExperienceGained;

        private Tweener m_TwExperienceIncrease;
        private int     m_Experience;
        private int     m_CurrentLevel;

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
            if (m_TwExperienceIncrease.IsActive()) m_TwExperienceIncrease.Kill();
            var value    = gemData.ExpValue;
            var newValue = m_Experience + value;
            m_TwExperienceIncrease = DOTween.To(() => m_Experience, x => m_Experience = x, newValue, value * m_ExperienceVariables.GainExperienceDuration)
                                            .OnUpdate(ExperienceGained)
                                            .OnKill(() => m_Experience = newValue);
        }

        private void ExperienceGained()
        {
            var nextLevelLimit = m_ExperienceVariables.ExperienceLevels[m_CurrentLevel];
            OnExperienceGained?.Invoke((float) m_Experience/nextLevelLimit);
            
            if (m_Experience < nextLevelLimit) return;
            if (m_TwExperienceIncrease.IsActive()) m_TwExperienceIncrease.Kill();
            m_Experience -= nextLevelLimit;
            OnExperienceGained?.Invoke((float) m_Experience /nextLevelLimit);
            LevelUp();
        }

        private void LevelUp()
        {
            m_CurrentLevel += 1;
            OnLevelUp?.Invoke();
        }
    }
}