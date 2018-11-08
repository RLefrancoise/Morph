using Morph.Input.Controllers.Features;
using UnityEngine;

namespace Morph.Input.Controllers.Oculus
{
    /// <inheritdoc />
    /// <summary>
    /// Oculus tracked remote controller
    /// </summary>
    class MorphOculusTrackedRemoteController : MorphAbstractController
    {
        public override MorphControllerFeatures SupportedFeatures =>
            MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking |
            MorphControllerFeatures.TouchPad;

        public OVRTrackedRemote TrackedRemote { get; protected set; }

        protected override void Awake()
        {
            base.Awake();

            TrackedRemote = GetComponent<OVRTrackedRemote>();

            TouchPad.TouchPads = new[]
            {
                new MorphTouchPadData(0, 0)
            };
        }

        protected override void UpdateTouchPad()
        {
            base.UpdateTouchPad();

            if (!OVRInput.IsControllerConnected(TrackedRemote.m_controller))
            {
                TouchPad.TouchPads[0].HorizontalAxisValue = 0f;
                TouchPad.TouchPads[0].VerticalAxisValue = 0f;
            }
            else
            {
                Vector2 touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, TrackedRemote.m_controller);
                TouchPad.TouchPads[0].HorizontalAxisValue = touchPad.x;
                TouchPad.TouchPads[0].VerticalAxisValue = touchPad.y;
            }
        }
    }
}
