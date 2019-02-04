using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <summary>
    /// Interface for Morph controller button
    /// </summary>
    public interface IMorphControllerButton
    {
        /// <summary>
        /// Button name
        /// </summary>
        string ButtonName { get; }
        /// <summary>
        /// Is button pressed
        /// </summary>
        bool Pressed { get; }

        /// <summary>
        /// Button pressed event
        /// </summary>
        event EventHandler<bool> ButtonPressed;
    }
}
