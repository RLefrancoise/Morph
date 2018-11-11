using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Buttons;
using Morph.Input.Controllers.Features.Gestures;
using UnityEngine;

namespace Morph.Input.Controllers.Oculus
{
    /// <inheritdoc />
    /// <summary>
    /// Oculus tracked remote controller
    /// </summary>
    public class MorphOculusTrackedRemoteController : MorphAbstractController
    {
        public override MorphControllerFeatures SupportedFeatures =>
            MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking |
            MorphControllerFeatures.TouchPad | MorphControllerFeatures.Buttons | MorphControllerFeatures.Gestures | MorphControllerFeatures.Haptics;

        public OVRTrackedRemote TrackedRemote { get; protected set; }

        protected MorphControllerGestureSwipe SwipeGesture { get; set; }

        protected override void Awake()
        {
            base.Awake();

            TrackedRemote = GetComponent<OVRTrackedRemote>();

            //Touchpad
            TouchPad.TouchPads = new[]
            {
                new MorphTouchPadData(0, 0)
            };

            //Buttons
            Buttons.Buttons = new[]
            {
                new MorphControllerButton("Back")
            };

            Buttons.Triggers = new[]
            {
                new MorphControllerTriggerButton("PrimaryIndexTrigger")
            };

            //Gestures
            SwipeGesture = new MorphControllerGestureSwipe();
            Gestures.Gestures = new MorphControllerGesture[]
            {
                SwipeGesture
            };

            //Haptics
            Haptics.HapticSystem = new MorphOculusHapticSystem(TrackedRemote.m_controller);
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
            if (!OVRInput.IsControllerConnected(TrackedRemote.m_controller)) return;

            //Swipe
            MorphControllerGestureSwipe.SwipeDirections swipeDirections = 0;

            if (OVRInput.GetDown(OVRInput.Button.DpadUp)) swipeDirections |= MorphControllerGestureSwipe.SwipeDirections.SwipeUp;
            if (OVRInput.GetDown(OVRInput.Button.DpadDown)) swipeDirections |= MorphControllerGestureSwipe.SwipeDirections.SwipeDown;
            if (OVRInput.GetDown(OVRInput.Button.DpadLeft)) swipeDirections |= MorphControllerGestureSwipe.SwipeDirections.SwipeLeft;
            if (OVRInput.GetDown(OVRInput.Button.DpadRight)) swipeDirections |= MorphControllerGestureSwipe.SwipeDirections.SwipeRight;

            if (swipeDirections != 0)
            {
                SwipeGesture.Directions = swipeDirections;
                SwipeGesture.Recognize();
            }
        }
    }
}
