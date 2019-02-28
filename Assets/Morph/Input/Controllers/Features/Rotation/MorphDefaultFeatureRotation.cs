using UnityEngine;

namespace Morph.Input.Controllers.Features.Rotation
{
    /// <inheritdoc />
    /// <summary>
    /// Morph default rotation feature
    /// </summary>
    public class MorphDefaultFeatureRotation : MorphFeatureRotation
    {
        private Quaternion? _previousRotation;

        public void Update(Transform transform)
        {
            Rotation = transform.rotation;
            ForwardDirection = transform.forward;
            UpDirection = transform.up;
            RightDirection = transform.right;

            if (_previousRotation.HasValue)
            {
                var delta = Rotation * Quaternion.Inverse(_previousRotation.Value);
                RotationDelta = delta;

                float angle;
                Vector3 axis;
                
                delta.ToAngleAxis(out angle, out axis);

                angle *= Mathf.Deg2Rad;

                AngularSpeed = axis * angle * (1.0f / Time.deltaTime);
            }

            _previousRotation = Rotation;
        }
    }
}
