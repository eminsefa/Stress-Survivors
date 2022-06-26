using System.Collections.Generic;
using UnityEngine;

namespace StressSurvivors
{
    public static class Helper
    {
        
    }
    public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        protected List<TKey> keyData = new ();

        protected List<TValue> valueData = new ();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            //Debug.LogError(this.keyData.Count + "   " + valueData.Count);

            for (int i = 0; i < this.keyData.Count; i++)
            {
                this[keyData[i]] = valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            keyData.Clear();
            valueData.Clear();

            foreach (var entry in this)
            {
                keyData.Add(entry.Key);
                valueData.Add(entry.Value);
            }
        }
    }
}