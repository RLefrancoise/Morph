using System;

namespace Morph.Components.Interaction
{
    /// <inheritdoc />
    /// <summary>
    /// Base interface for Morph component grab interaction
    /// </summary>
    public interface IMorphComponentGrab : IMorphComponentInteraction
    {
        /// <summary>
        /// Is component grabbed ?
        /// </summary>
        bool IsGrabbed { get; }

        /// <summary>
        /// Grab
        /// </summary>
        void Grab();

        /// <summary>
        /// Release
        /// </summary>
        void Release();

        /// <summary>
        /// Called when grabbed
        /// </summary>
        event EventHandler Grabbed;

        /// <summary>
        /// Called when released
        /// </summary>
        event EventHandler Released;
    }
}
