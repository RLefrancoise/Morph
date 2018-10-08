using UnityEngine;

namespace Morph.Other
{
    /// <inheritdoc />
    /// <summary>
    /// Unity singleton template
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!_instance) _instance = FindObjectOfType<T>();
                return _instance;
            }
        }

        void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}
