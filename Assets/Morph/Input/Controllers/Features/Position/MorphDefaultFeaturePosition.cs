using UnityEngine;

namespace Morph.Input.Controllers.Features.Position
{
    public class MorphDefaultFeaturePosition : MorphFeaturePosition
    {
        protected Vector3? _previousPosition;

        public void Update(Transform transform)
        {
            Position = transform.position;

            if (_previousPosition.HasValue)
            {
                Vector3 delta = Position - _previousPosition.Value;
                MovementDirection = delta.normalized;
                PositionDelta = delta;
                Speed = delta / Time.deltaTime;
            }

            _previousPosition = Position;
        }
    }
}
