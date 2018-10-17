using UnityEngine;

namespace Morph.Core
{
    /// <summary>
    /// Interface for any display in morph application
    /// </summary>
    public interface IMorphDisplay
    {
        /// <summary>
        /// Type of display
        /// </summary>
        MorphDisplayType DisplayType { get; }

        /// <summary>
        /// Unity camera
        /// </summary>
        Camera Camera { get; }
    }
}
