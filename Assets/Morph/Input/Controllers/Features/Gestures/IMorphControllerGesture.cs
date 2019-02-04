using System;

namespace Morph.Input.Controllers.Features.Gestures
{
    /// <summary>
    /// Interface for Morph controller gesture
    /// </summary>
    public interface IMorphControllerGesture
    {
        /// <summary>
        /// Gesture type
        /// </summary>
        MorphControllerGestureType GestureType { get; }

        /// <summary>
        /// Called when gesture is recognized
        /// </summary>
        event EventHandler Recognized;
    }
}
