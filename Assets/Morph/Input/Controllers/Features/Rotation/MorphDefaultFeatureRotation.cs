using UnityEngine;

namespace Morph.Input.Controllers.Features.Rotation
{
    public class MorphDefaultFeatureRotation : MorphFeatureRotation
    {
        protected Vector3? _previousRotation;

        public void Update(Transform transform)
        {
            Rotation = transform.rotation;
            ForwardDirection = transform.forward;
            UpDirection = transform.up;
            RightDirection = transform.right;

            if (_previousRotation.HasValue)
            {
                Vector3 delta = Rotation.eulerAngles - _previousRotation.Value;
                RotationDelta = delta;
                AngularSpeed = delta / Time.deltaTime;
            }

            _previousRotation = Rotation.eulerAngles;
        }
    }
}
