using UnityEngine;

namespace StressSurvivors
{
    public abstract class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance) return _instance;

#if UNITY_EDITOR
                var instances = FindObjectsOfType<T>();

                if (instances.Length == 0)
                    return default;

                if (instances.Length > 1)
                    for (var i = instances.Length - 1; i >= 1; i--)
                    {
                        var o = instances[i];
                        Debug.LogWarning($"There are multiple instances in the scene. Destroying {o.name}", o);
                        Destroy(o);
                    }

                _instance = instances[0];
#else
                _instance = FindObjectOfType<T>();
#endif

                return _instance;
            }
        }
    }
}