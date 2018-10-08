using System;

namespace Morph.Components.Interaction
{
    /// <inheritdoc />
    /// <summary>
    /// Base interface for Morph component focus interaction
    /// </summary>
    public interface IMorphComponentFocus : IMorphComponentInteraction
    {
        /// <summary>
        /// Focus
        /// </summary>
        void Focus();

        /// <summary>
        /// Unfocus
        /// </summary>
        void Unfocus();

        /// <summary>
        /// Called when entered focus
        /// </summary>
        event EventHandler EnteredFocus;
        /// <summary>
        /// Called when exited focus
        /// </summary>
        event EventHandler ExitedFocus;
    }
}
