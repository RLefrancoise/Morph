using System;

namespace Morph.Input.Controllers.Features.Gestures
{
    /// <summary>
    /// Morph controller gesture base
    /// </summary>
    public abstract class MorphControllerGesture
    {
        /// <summary>
        /// Gesture type
        /// </summary>
        public MorphControllerGestureType GestureType { get; }

        /// <summary>
        /// Called when gesture is recognized
        /// </summary>
        public EventHandler Recognized;

        protected MorphControllerGesture(MorphControllerGestureType gestureType)
        {
            GestureType = gestureType;
        }

        /// <summary>
        /// Make gesture recognized
        /// </summary>
        internal virtual void Recognize()
        {
            Recognized?.Invoke(this, EventArgs.Empty);
        }
    }
}
