using System;
using Morph.Components;
using Morph.Components.Interaction;
using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Buttons;
using Morph.Input.Controllers.Features.Gestures;
using Morph.Input.Controllers.Features.Touchpad;
using Morph.Input.Controllers.Oculus.Features.Gestures;
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

        private MorphTouchpad _primaryTouchpad;
        private MorphFeatureTouchpads _touchpads;

        private MorphFeatureGestures _gestures;

        public override MorphFeatureButtons Buttons => _buttons;
        public override MorphFeatureTouchpads Touchpads => _touchpads;
        public override MorphFeatureGestures Gestures => _gestures;
        public override MorphFeatureHaptics Haptics => null;
        public override MorphFeatureWarp Warp => null;
        
        [SerializeField]
        private GameObject _reticlePrefab;

        public override MorphControllerFeatures SupportedFeatures =>
            MorphControllerFeatures.Position | MorphControllerFeatures.Rotation |
            MorphControllerFeatures.TouchPad | MorphControllerFeatures.Buttons | MorphControllerFeatures.Gestures | MorphControllerFeatures.ThreeDegreesOfFreedom;

        public OVRTrackedRemote TrackedRemote { get; protected set; }

        protected MorphControllerGestureSwipe SwipeGesture { get; set; }

        protected GameObject LastHitComponent { get; set; }
        protected float GrabbedComponentInitialDistance { get; set;}

        protected GameObject Reticle { get; set; }
        protected GameObject ControllerGameObject { get; set; }
        protected LineRenderer LineRenderer { get; set; }

        public override bool Initialize()
        {
            //Buttons
            _backButton = new MorphControllerButton("Back");

            _primaryIndexTrigger = new MorphControllerTriggerButton("PrimaryIndexTrigger");
            _primaryIndexTrigger.TriggerValueChanged += ListenTriggerValue; //Listen for select & grab

            _buttons = new MorphFeatureButtons(
                new IMorphControllerButton[]
                {
                    _backButton
                },
                new IMorphControllerTriggerButton[]
                {
                    _primaryIndexTrigger
                });

            //Touchpad
            _primaryTouchpad = new MorphTouchpad();
            _primaryTouchpad.TouchpadClicked += ListenTouchpadClicked; //Listen touchpad for select & grab

            _touchpads = new MorphFeatureTouchpads(new IMorphTouchpad[]
            {
                _primaryTouchpad
            });

            //Gestures
            SwipeGesture = new MorphOculusControllerGestureSwipe(TrackedRemote.m_controller);

            _gestures = new MorphFeatureGestures(new IMorphControllerGesture[] {SwipeGesture});

            return base.Initialize();
        }

        protected override void Awake()
        {
            base.Awake();

            TrackedRemote = GetComponent<OVRTrackedRemote>();
            LineRenderer = GetComponent<LineRenderer>();
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
                float verticalDelta = Touchpads.Touchpads[0].VerticalAxisDelta;
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
                _primaryTouchpad.HorizontalAxisValue = 0f;
                _primaryTouchpad.VerticalAxisValue = 0f;
                _primaryTouchpad.HorizontalAxisDelta = 0f;
                _primaryTouchpad.VerticalAxisDelta = 0f;

                if(_primaryTouchpad.Clicked) _primaryTouchpad.Clicked = false;
            }
            else
            {
                if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
                {
                    Vector2 touchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, TrackedRemote.m_controller);

                    if (_primaryTouchpad.HorizontalAxisValue == 0f)
                        _primaryTouchpad.HorizontalAxisDelta = 0f;
                    else
                        _primaryTouchpad.HorizontalAxisDelta = touchPad.x - Touchpads.Touchpads[0].HorizontalAxisValue;

                    if (_primaryTouchpad.VerticalAxisValue == 0f)
                        _primaryTouchpad.VerticalAxisDelta = 0f;
                    else
                        _primaryTouchpad.VerticalAxisDelta = touchPad.y - Touchpads.Touchpads[0].VerticalAxisValue;

                    _primaryTouchpad.HorizontalAxisValue = touchPad.x;
                    _primaryTouchpad.VerticalAxisValue = touchPad.y;
                }
                else
                {
                    _primaryTouchpad.HorizontalAxisDelta = 0f;
                    _primaryTouchpad.VerticalAxisDelta = 0f;
                    _primaryTouchpad.HorizontalAxisValue = 0f;
                    _primaryTouchpad.VerticalAxisValue = 0f;
                }
                
                //Is touchpad clicked ?
                if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
                {
                    _primaryTouchpad.Clicked = true;
                }
                //Is touchped released ?
                else if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
                {
                    _primaryTouchpad.Clicked = false;
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
            SwipeGesture.Update();
        }
    }
}
