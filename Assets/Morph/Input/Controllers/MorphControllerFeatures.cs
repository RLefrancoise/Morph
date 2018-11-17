using System;

namespace Morph.Input.Controllers
{
    /// <summary>
    /// Features for controllers
    /// </summary>
    [Flags]
    public enum MorphControllerFeatures
    {
        /// <summary>
        /// No feature
        /// </summary>
        None = 0,
        /// <summary>
        /// Position tracking
        /// </summary>
        PositionTracking = 1 << 0,
        /// <summary>
        /// Rotation tracking
        /// </summary>
        RotationTracking = 1 << 1,
        /// <summary>
        /// Touchpad
        /// </summary>
        TouchPad = 1 << 2,
        /// <summary>
        /// Buttons
        /// </summary>
        Buttons = 1 << 3,
        /// <summary>
        /// Gestures
        /// </summary>
        Gestures = 1 << 4,
        /// <summary>
        /// Haptics
        /// </summary>
        Haptics = 1 << 5
    }
}
