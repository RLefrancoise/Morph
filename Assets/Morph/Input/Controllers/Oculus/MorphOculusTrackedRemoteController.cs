using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Buttons;
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
            MorphControllerFeatures.TouchPad | MorphControllerFeatures.Buttons | MorphControllerFeatures.Gestures;

        public OVRTrackedRemote TrackedRemote { get; protected set; }

        protected override void Awake()
        {
            base.Awake();

            TrackedRemote = GetComponent<OVRTrackedRemote>();

            TouchPad.TouchPads = new[]
            {
                new MorphTouchPadData(0, 0)
            };

            Buttons.Buttons = new[]
            {
                new MorphControllerButton("Back")
            };

            Buttons.Triggers = new[]
            {
                new MorphControllerTriggerButton("PrimaryIndexTrigger")
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

                //Is touchpad clicked ?
                if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
                {
                    TouchPad.TouchPads[0].Clicked = true;
                }
            }
        }

        protected override void UpdateButtons()
        {
            base.UpdateButtons();

            if (!OVRInput.IsControllerConnected(TrackedRemote.m_controller))
            {
                //Back button
                Buttons.Buttons[0].Pressed = false;

                //Index trigger
                Buttons.Triggers[0].TriggerValue = 0f;
            }
            else
            {
                //Back button
                Buttons.Buttons[0].Pressed = OVRInput.GetDown(OVRInput.Button.Back, TrackedRemote.m_controller);

                //Index trigger
                Buttons.Triggers[0].TriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, TrackedRemote.m_controller);
            }
        }

        protected override void UpdateGestures()
        {
            
        }
    }
}
