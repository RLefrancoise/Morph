using System;

namespace Morph.Input.Controllers.Features.Gestures
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller swipe gesture
    /// </summary>
    public class MorphControllerGestureSwipe : MorphControllerGesture
    {
        /// <summary>
        /// Swipe type
        /// </summary>
        [Flags]
        public enum SwipeDirections
        {
            /// <summary>
            /// Swipe Up
            /// </summary>
            SwipeUp = 1,
            /// <summary>
            /// Swipe Down
            /// </summary>
            SwipeDown = 1 << 1,
            /// <summary>
            /// Swipe Left
            /// </summary>
            SwipeLeft = 1 << 2,
            /// <summary>
            /// Swipe Right
            /// </summary>
            SwipeRight = 1 << 3
        }

        /// <summary>
        /// Swipe directions of the recognized swipe
        /// </summary>
        public SwipeDirections Directions { get; internal set; }

        public MorphControllerGestureSwipe() : base(MorphControllerGestureType.Swipe)
        {
        }
    }
}
