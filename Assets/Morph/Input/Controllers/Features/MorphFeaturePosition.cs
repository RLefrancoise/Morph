using UnityEngine;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Position feature
    /// </summary>
    public abstract class MorphFeaturePosition : IMorphControllerFeature
    {
        /// <summary>
        /// Position in world space
        /// </summary>
        public Vector3 Position { get; protected set; }

        /// <summary>
        /// Position delta in world space
        /// </summary>
        public Vector3 PositionDelta { get; protected set; }

        /// <summary>
        /// Current speed on each axis
        /// </summary>
        public Vector3 Speed { get; protected set; }

        /// <summary>
        /// Movement direction
        /// </summary>
        public Vector3 MovementDirection { get; protected set; }
    }
}
