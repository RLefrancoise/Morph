using Morph.Components;
using UnityEngine;

namespace Morph.Core
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for any display in morph application
    /// </summary>
    public interface IMorphDisplay : IMorphComponent
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
