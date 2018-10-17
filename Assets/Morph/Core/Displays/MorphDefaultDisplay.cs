using UnityEngine;

namespace Morph.Core.Displays
{
    /// <inheritdoc cref="IMorphDisplay"/>
    /// <summary>
    /// Default Morph camera
    /// </summary>
    public class MorphDefaultDisplay : MonoBehaviour, IMorphDisplay
    {
        public MorphDisplayType DisplayType { get; private set; }
        public Camera Camera { get; private set; }

        void Awake()
        {
            DisplayType = MorphDisplayType.Opaque;
            Camera = GetComponent<Camera>();
        }
    }
}
