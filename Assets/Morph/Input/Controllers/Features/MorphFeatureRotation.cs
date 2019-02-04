using UnityEngine;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Rotation feature
    /// </summary>
    public class MorphFeatureRotation : IMorphControllerFeature
    {
        /// <summary>
        /// Rotation in world space
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// The direction the controller is looking at
        /// </summary>
        public Vector3 ForwardDirection { get; set; }

        /// <summary>
        /// The up direction of the controller
        /// </summary>
        public Vector3 UpDirection { get; set; }

        /// <summary>
        /// The right direction of the controller
        /// </summary>
        public Vector3 RightDirection { get; set; }

        /// <summary>
        /// Rotation speed on each axis
        /// </summary>
        public Vector3 AngularSpeed { get; set; }

        /// <summary>
        /// Rotation delta between current rotation and previous one
        /// </summary>
        public Vector3 RotationDelta { get; set; }
    }
}
