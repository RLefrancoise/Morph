using UnityEngine;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Position tracking feature
    /// </summary>
    public class MorphFeaturePositionTracking : IMorphControllerFeature
    {
        /// <summary>
        /// Position in world space
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Position delta in world space
        /// </summary>
        public Vector3 PositionDelta { get; set; }

        /// <summary>
        /// Current speed on each axis
        /// </summary>
        public Vector3 Speed { get; set; }

        /// <summary>
        /// Movement direction
        /// </summary>
        public Vector3 MovementDirection { get; set; }
    }
}
