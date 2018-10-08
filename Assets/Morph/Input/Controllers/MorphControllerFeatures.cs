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
        RotationTracking = 1 << 1
    }
}
