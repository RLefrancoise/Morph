using UnityEngine;

namespace Morph.Core.Cameras
{
    /// <inheritdoc cref="IMorphCamera"/>
    /// <summary>
    /// Default Morph camera
    /// </summary>
    public class MorphDefaultCamera : MonoBehaviour, IMorphCamera
    {
        public Camera Camera { get; private set; }

        void Awake()
        {
            Camera = GetComponent<Camera>();
        }
    }
}
