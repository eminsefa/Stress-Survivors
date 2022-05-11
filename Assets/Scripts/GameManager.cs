using System;

namespace StressSurvivors
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameReset;

        public void LevelFailed()
        {
            OnGameReset?.Invoke();
        }
    }
}
