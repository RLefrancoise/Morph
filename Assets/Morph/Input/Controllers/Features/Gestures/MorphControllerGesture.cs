using System;

namespace Morph.Input.Controllers.Features.Gestures
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller gesture base
    /// </summary>
    public abstract class MorphControllerGesture : IMorphControllerGesture
    {
        public MorphControllerGestureType GestureType { get; }

        /// <summary>
        /// Called when gesture is recognized
        /// </summary>
        public event EventHandler Recognized;

        protected MorphControllerGesture(MorphControllerGestureType gestureType)
        {
            GestureType = gestureType;
        }

        /// <summary>
        /// Update the gesture
        /// </summary>
        public abstract void Update();
        
        /// <summary>
        /// Make gesture recognized
        /// </summary>
        protected virtual void Recognize()
        {
            Recognized?.Invoke(this, EventArgs.Empty);
        }
    }
}
