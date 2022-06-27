using System;
using UnityEngine;

namespace StressSurvivors
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action OnGameReset;

        private void OnEnable()
        {
            ExperienceManager.OnLevelUp += OnLevelUp;
            LevelUpScreen.OnBuffSelected  += OnBuffSelected;
        }

        private void OnDisable()
        {
            ExperienceManager.OnLevelUp  -= OnLevelUp;
            LevelUpScreen.OnBuffSelected -= OnBuffSelected;
        }

        private void OnLevelUp()
        {
            Time.timeScale = 0;
        }

        private void OnBuffSelected()
        {
            Time.timeScale = 1;
        }

        public void LevelFailed()
        {
            OnGameReset?.Invoke();
        }
    }
}
