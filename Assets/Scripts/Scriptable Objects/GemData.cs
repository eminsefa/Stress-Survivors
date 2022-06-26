using Sirenix.OdinInspector;
using UnityEngine;

namespace StressSurvivors
{
    [InlineEditor]
    [CreateAssetMenu]
    public class GemData : ScriptableObject
    {
        public int      ExpValue;
        public EGemType GemType;
        public Sprite   Icon;
        public int      MinPossibility;
    }

    public enum EGemType
    {
        Blue,
        Yellow,
        Green,
        Red
    }
}
