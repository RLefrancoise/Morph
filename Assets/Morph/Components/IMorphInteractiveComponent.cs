using UnityEngine;

namespace Morph.Components
{
    /// <inheritdoc />
    /// <summary>
    /// Base interface for any Morph interactive component
    /// </summary>
    public interface IMorphInteractiveComponent : IMorphComponent
    {
        /// <summary>
        /// Is interactable ?
        /// </summary>
        bool Interactable { get; set; }

        /// <summary>
        /// Collider of the component
        /// </summary>
        Collider Collider { get; }
    }
}
