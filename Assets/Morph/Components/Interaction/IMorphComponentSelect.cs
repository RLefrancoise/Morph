using System;

namespace Morph.Components.Interaction
{
    /// <inheritdoc />
    /// <summary>
    /// Base interface for Morph component select interaction
    /// </summary>
    public interface IMorphComponentSelect : IMorphComponentInteraction
    {
        /// <summary>
        /// Is selected ?
        /// </summary>
        bool IsSelected { get; }

        /// <summary>
        /// Select
        /// </summary>
        void Select();

        /// <summary>
        /// Deselect
        /// </summary>
        void Deselect();

        /// <summary>
        /// Called when selected
        /// </summary>
        event EventHandler Selected;

        /// <summary>
        /// Called when deselected
        /// </summary>
        event EventHandler Deselected;
    }
}
