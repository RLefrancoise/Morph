using UnityEngine;

namespace Morph.Components.Navigation
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for Morph player controller
    /// </summary>
    public interface IMorphPlayerController : IMorphComponent
    {
        /// <summary>
        /// Transform of the player
        /// </summary>
        Transform PlayerTransform { get; }
        /// <summary>
        /// Camera of the player
        /// </summary>
        Camera PlayerCamera { get; }
    }
}