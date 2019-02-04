using Morph.Input.Controllers.Features.Gestures;

namespace Morph.Input.Controllers.Oculus.Features.Gestures
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller swipe gesture for Oculus controllers
    /// </summary>
    public class MorphOculusControllerGestureSwipe : MorphControllerGestureSwipe
    {
        /// <summary>
        /// Controller of gesture
        /// </summary>
        public OVRInput.Controller Controller { get; }

        public MorphOculusControllerGestureSwipe(OVRInput.Controller controller)
        {
            Controller = controller;
        }

        public override void Update()
        {
            if (!OVRInput.IsControllerConnected(Controller)) return;

            //Swipe
            SwipeDirections swipeDirections = 0;

            if (OVRInput.GetDown(OVRInput.Button.DpadUp)) swipeDirections |= SwipeDirections.SwipeUp;
            if (OVRInput.GetDown(OVRInput.Button.DpadDown)) swipeDirections |= SwipeDirections.SwipeDown;
            if (OVRInput.GetDown(OVRInput.Button.DpadLeft)) swipeDirections |= SwipeDirections.SwipeLeft;
            if (OVRInput.GetDown(OVRInput.Button.DpadRight)) swipeDirections |= SwipeDirections.SwipeRight;

            if (swipeDirections == 0) return;

            Directions = swipeDirections;
            Recognize();
        }
    }
}
