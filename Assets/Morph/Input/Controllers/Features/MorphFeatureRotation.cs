using UnityEngine;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Rotation feature
    /// </summary>
    public abstract class MorphFeatureRotation : IMorphControllerFeature
    {
        /// <summary>
        /// Rotation in world space
        /// </summary>
        public Quaternion Rotation { get; protected set; }

        /// <summary>
        /// The direction the controller is looking at
        /// </summary>
        public Vector3 ForwardDirection { get; protected set; }

        /// <summary>
        /// The up direction of the controller
        /// </summary>
        public Vector3 UpDirection { get; protected set; }

        /// <summary>
        /// The right direction of the controller
        /// </summary>
        public Vector3 RightDirection { get; protected set; }

        /// <summary>
        /// Rotation speed on each axis
        /// </summary>
        public Vector3 AngularSpeed { get; protected set; }

        /// <summary>
        /// Rotation delta between current rotation and previous one
        /// </summary>
        public Quaternion RotationDelta { get; protected set; }
    }
}
