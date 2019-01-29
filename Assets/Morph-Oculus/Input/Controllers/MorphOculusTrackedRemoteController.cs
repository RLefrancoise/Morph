using System;
using Morph.Components;
using Morph.Components.Interaction;
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
    [RequireComponent(typeof(LineRenderer))]
    public class MorphOculusTrackedRemoteController : MorphAbstractController
    {
        private MorphFeatureButtons _buttons;
        private MorphControllerButton _backButton;

        private MorphControllerTriggerButton _primaryIndexTrigger;

        public override MorphFeatureButtons Buttons => _buttons;

        [SerializeField]
        private GameObject _reticlePrefab;

        public override MorphControllerFeatures SupportedFeatures =>
            MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking |
            MorphControllerFeatures.TouchPad | MorphControllerFeatures.Buttons | MorphControllerFeatures.Gestures;

        public OVRTrackedRemote TrackedRemote { get; protected set; }

        protected MorphControllerGestureSwipe SwipeGesture { get; set; }

        protected GameObject LastHitComponent { get; set; }
        protected float GrabbedComponentInitialDistance { get; set;}

        protected GameObject Reticle { get; set; }
        protected GameObject ControllerGameObject { get; set; }
        protected LineRenderer LineRenderer { get; set; }

        public override bool Initialize()
        {
            if (!base.Initialize()) return false;

            //Buttons
            _backButton = new MorphControllerButton("Back");

            _primaryIndexTrigger = new MorphControllerTriggerButton("PrimaryIndexTrigger");

            _buttons = new MorphFeatureButtons(new[]
                {
                    _backButton
                },
                new[]
                {
                    _primaryIndexTrigger
                });

            return true;
        }

        protected override void Awake()
        {
            base.Awake();

            TrackedRemote = GetComponent<OVRTrackedRemote>();
            LineRenderer = GetComponent<LineRenderer>();

            //Touchpad
            TouchPad.TouchPads = new[]
            {
                new MorphTouchPadData(0, 0)
            };

            //Gestures
            SwipeGesture = new MorphControllerGestureSwipe();
            Gestures.Gestures = new MorphControllerGesture[]
            {
                SwipeGesture
            };

            //Listen touchpad and trigger for select & grab
            TouchPad.TouchPads[0].TouchpadClicked += ListenTouchpadClicked;
            Buttons.Triggers[0].TriggerValueChanged += ListenTriggerValue;
        }

        protected override void BeforeUpdate()
        {
            base.BeforeUpdate();

            if (TrackedRemote.m_modelOculusGoController.activeInHierarchy || TrackedRemote.m_modelGearVrController.activeInHierarchy)
            {
                ControllerGameObject = TrackedRemote.m_modelOculusGoController.activeInHierarchy
                    ? TrackedRemote.m_modelOculusGoController
                    : TrackedRemote.m_modelGearVrController;
            }
            else
            {
                ControllerGameObject = null;
            }

            //If no reticle and controller visible
            if (!Reticle && _reticlePrefab && ControllerGameObject)
            {
                Reticle = Instantiate(_reticlePrefab, transform);
            }
            //If no controller visible and reticle
            else if (Reticle && !ControllerGameObject)
            {
                Destroy(Reticle);
                Reticle = null;
            }

            //Hide line if no reticle
            LineRenderer.enabled = Reticle != null;
        }

        protected override void AfterUpdate()
        {
            base.AfterUpdate();

            //Interactions
            RaycastHit hit;
            if (Physics.Raycast(Position.Position, Rotation.ForwardDirection, out hit))
            {
                //Move reticle
                if (Reticle)
                {
                    Reticle.transform.position = hit.point;
                    Reticle.transform.rotation = Quaternion.LookRotation(hit.normal);
                    Reticle.transform.Translate(Vector3.forward * 0.01f, Space.Self);
                }

                //If new component hit
                if (hit.transform.gameObject != LastHitComponent)
                {
                    CancelInteractions();

                    //Store new hit component
                    LastHitComponent = hit.transform.gameObject;

                    //If gameobject is not interactive, ignore it
                    if (LastHitComponent.GetComponent<IMorphInteractiveComponent>() == null) return;

                    //Focus
                    var focus = LastHitComponent?.GetComponent<IMorphComponentFocus>();
                    if (focus != null && !focus.IsFocused) focus.Focus();
                }
            }
            else
            {
                CancelInteractions();
                LastHitComponent = null;

                if (Reticle)
                {
                    Reticle.transform.localPosition = Vector3.zero;
                    Reticle.transform.localRotation = Quaternion.identity;
                    Reticle.transform.Translate(Vector3.forward * 5f, Space.Self);
                }
            }

            //Move line
            if (LineRenderer.enabled)
            {
                LineRenderer.SetPosition(0, Vector3.zero);
                LineRenderer.SetPosition(1, Reticle.transform.localPosition);
            }

            //If component grabbed, update its position
            var grab = LastHitComponent?.GetComponent<IMorphComponentGrab>();
            if (LastHitComponent && grab != null && grab.IsGrabbed)
            {
                LastHitComponent.transform.RotateAround(transform.position, Rotation.UpDirection, Rotation.RotationDelta.y);
                LastHitComponent.transform.RotateAround(transform.position, Rotation.RightDirection, Rotation.RotationDelta.x);
                LastHitComponent.transform.RotateAround(transform.position, Rotation.ForwardDirection, Rotation.RotationDelta.z);

                //Look for touchpad delta to move grabbed component closer or farer
                float verticalDelta = TouchPad.TouchPads[0].VerticalAxisDelta;
                LastHitComponent.transform.Translate(transform.forward * GrabbedComponentInitialDistance * verticalDelta);
            }
        }

        private void CancelInteractions()
        {
            if (LastHitComponent && LastHitComponent.GetComponent<IMorphInteractiveComponent>() != null)
            {
                //Unfocus
                IMorphComponentFocus focus = LastHitComponent?.GetComponent<IMorphComponentFocus>();
                if (focus != null && focus.IsFocused) focus.Unfocus();

                //Deselect
                IMorphComponentSelect select = LastHitComponent?.GetComponent<IMorphComponentSelect>();
                if (select != null && select.IsSelected) select.Deselect();

                //Release
                IMorphComponentGrab grab = LastHitComponent?.GetComponent<IMorphComponentGrab>();
                if (grab != null && grab.IsGrabbed) grab.Release();
            }
        }

        protected void ListenTouchpadClicked(object sender, bool clicked)
        {
            if (!LastHitComponent || LastHitComponent.GetComponent<IMorphInteractiveComponent>() == null) return;

            //Select
            IMorphComponentSelect select = LastHitComponent.GetComponent<IMorphComponentSelect>();
            if (select == null) return;

            if (clicked && !select.IsSelected)
            {
                select.Select();
            }
            else if (!clicked && select.IsSelected)
            {
                select.Deselect();
            }
        }

        protected void ListenTriggerValue(object sender, float value)
        {
            if(!LastHitComponent || LastHitComponent.GetComponent<IMorphInteractiveComponent>() == null) return;
            
            //Grab
            IMorphComponentGrab grab = LastHitComponent.GetComponent<IMorphComponentGrab>();
            if (grab == null) return;

            if (value >= 0.9f && !grab.IsGrabbed)
            {
                grab.Grab();
                //Store initial distance between object and controller
                GrabbedComponentInitialDistance = Vector3.Distance(LastHitComponent.transform.position, transform.position);
            }
            else if (value < 0.1f && grab.IsGrabbed)
            {
                grab.Release();
            }
        }

        protected override void UpdateTouchPad()
        {
            base.UpdateTouchPad();

            if (!OVRInput.IsControllerConnected(TrackedRemote.m_controller))
            {
                TouchPad.TouchPads[0].HorizontalAxisValue = 0f;
                TouchPad.TouchPads[0].VerticalAxisValue = 0f;
                TouchPad.TouchPads[0].HorizontalAxisDelta = 0f;
                TouchPad.TouchPads[0].VerticalAxisDelta = 0f;

                if(TouchPad.TouchPads[0].Clicked) TouchPad.TouchPads[0].Clicked = false;
            }
            else
            {
                if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
                {
                    Vector2 touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, TrackedRemote.m_controller);

                    if (TouchPad.TouchPads[0].HorizontalAxisValue == 0f)
                        TouchPad.TouchPads[0].HorizontalAxisDelta = 0f;
                    else
                        TouchPad.TouchPads[0].HorizontalAxisDelta = touchPad.x - TouchPad.TouchPads[0].HorizontalAxisValue;

                    if (TouchPad.TouchPads[0].VerticalAxisValue == 0f)
                        TouchPad.TouchPads[0].VerticalAxisDelta = 0f;
                    else
                        TouchPad.TouchPads[0].VerticalAxisDelta = touchPad.y - TouchPad.TouchPads[0].VerticalAxisValue;

                    TouchPad.TouchPads[0].HorizontalAxisValue = touchPad.x;
                    TouchPad.TouchPads[0].VerticalAxisValue = touchPad.y;
                }
                else
                {
                    TouchPad.TouchPads[0].HorizontalAxisDelta = 0f;
                    TouchPad.TouchPads[0].VerticalAxisDelta = 0f;
                    TouchPad.TouchPads[0].HorizontalAxisValue = 0f;
                    TouchPad.TouchPads[0].VerticalAxisValue = 0f;
                }
                
                //Is touchpad clicked ?
                if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
                {
                    TouchPad.TouchPads[0].Clicked = true;
                }
                //Is touchped released ?
                else if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
                {
                    TouchPad.TouchPads[0].Clicked = false;
                }
            }
        }

        protected override void UpdateButtons()
        {
            base.UpdateButtons();

            if (!OVRInput.IsControllerConnected(TrackedRemote.m_controller))
            {
                //Back button
                if(Buttons.Buttons[0].Pressed) _backButton.Pressed = false;

                //Index trigger
                if(Math.Abs(Buttons.Triggers[0].TriggerValue) > 0.01f) _primaryIndexTrigger.TriggerValue = 0f;
            }
            else
            {
                //Back button
                if(OVRInput.GetDown(OVRInput.Button.Back, TrackedRemote.m_controller))
                    _backButton.Pressed = true;
                else if (OVRInput.GetUp(OVRInput.Button.Back, TrackedRemote.m_controller))
                    _backButton.Pressed = false;

                //Index trigger
                _primaryIndexTrigger.TriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, TrackedRemote.m_controller);
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
