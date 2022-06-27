using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StressSurvivors
{
    public class Gem : MonoBehaviour
    {
        public static event Action<GemData> OnGemCollected;

        private GemVariables m_GemVariables => GameConfig.Instance.GemVariables;

        [field:SerializeField] public GemData GemData { get; private set; }

        public void Dropped()
        {
            transform.DOJump(new Vector3(Random.Range(1, 2f), Random.Range(1, 2f), 0),
                             m_GemVariables.DropJumpValue, 1, m_GemVariables.DropJumpDuration)
                     .SetRelative(true);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Collected();
        }

        private void Collected()
        {
            OnGemCollected?.Invoke(GemData);
            Destroy(gameObject); //Change with Pool
        }
    }
}