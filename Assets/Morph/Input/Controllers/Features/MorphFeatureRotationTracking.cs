using UnityEngine;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Rotation tracking feature
    /// </summary>
    public class MorphFeatureRotationTracking : IMorphControllerFeature
    {
        /// <summary>
        /// Rotation in world space
        /// </summary>
        public Quaternion Rotation { get; internal set; }

        /// <summary>
        /// The direction the controller is looking at
        /// </summary>
        public Vector3 ForwardDirection { get; internal set; }

        /// <summary>
        /// The up direction of the controller
        /// </summary>
        public Vector3 UpDirection { get; internal set; }

        /// <summary>
        /// The right direction of the controller
        /// </summary>
        public Vector3 RightDirection { get; internal set; }

        /// <summary>
        /// Rotation speed on each axis
        /// </summary>
        public Vector3 AngularSpeed { get; internal set; }

        /// <summary>
        /// Rotation delta between current rotation and previous one
        /// </summary>
        public Vector3 RotationDelta { get; internal set; }
    }
}
