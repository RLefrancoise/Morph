using System;
using Morph.Components.Interaction;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Input.Controllers.Common;
using Morph.Input.Controllers.Features;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.GoogleVR
{
    /// <inheritdoc />
    /// <summary>
    /// Morph GoogleVR reticle pointer
    /// </summary>
    public class MorphGoogleVrReticleController : MorphControllerWithEventTrigger<MorphComponentFocusWithEventTrigger, MorphComponentSelectWithEventTrigger, MorphComponentGrabWithEventTrigger>
    {
        /// <summary>
        /// Time elasped with trigger down
        /// </summary>
        private float _elapsedTimeWithTriggerDown;

        /// <summary>
        /// Was reticle trigger down last frame ?
        /// </summary>
        private bool _wasTriggerDownLastFrame;

        /// <summary>
        /// Time user needs to hold trigger before grab object
        /// </summary>
        [SerializeField]
        [Tooltip("Time user needs to hold trigger before grab object")]
        private float _waitTimeBeforeGrab = 1f;

        /// <summary>
        /// Google VR reticle pointer
        /// </summary>
        public GvrReticlePointer Reticle { get; private set; }

        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.Position | MorphControllerFeatures.Rotation;
        public override MorphFeatureTouchpads Touchpads => null;
        public override MorphFeatureButtons Buttons => null;
        public override MorphFeatureGestures Gestures => null;
        public override MorphFeatureHaptics Haptics => null;

        protected override Ray GrabbedRay => new Ray(transform.position, transform.forward);
        protected override Predicate<BaseEventData> SelectValidation => eventData => Reticle.TriggerDown;
        protected override Predicate<BaseEventData> DeselectValidation => eventData => Reticle.TriggerUp;
        protected override Predicate<BaseEventData> GrabValidation => eventData => _elapsedTimeWithTriggerDown >= _waitTimeBeforeGrab;
        protected override Predicate<BaseEventData> ReleaseValidation => DeselectValidation;

        protected override void Awake()
        {
            base.Awake();

            Reticle = GetComponent<GvrReticlePointer>();
        }

        public override void Visit(IMorphComponentGrab visitable)
        {
            //Override grab, we don't use event trigger for grab as Google VR reticle doesn't seem to handle begin drag/drag/end drag events
        }

        protected override void BeforeUpdate()
        {
            base.BeforeUpdate();

            //if no object is hit, no grab
            if (!Reticle.CurrentRaycastResult.isValid) return;

            //if hit object doesn't have grab component, no grab
            IMorphComponentGrab grab = Reticle.CurrentRaycastResult.gameObject.GetComponent<IMorphComponentGrab>();
            if (grab == null) return;

            //Is begin grab
            if (Reticle.TriggerDown && !_wasTriggerDownLastFrame)
            {
                _wasTriggerDownLastFrame = true;
                _elapsedTimeWithTriggerDown = 0;
            }

            //No trigger pressed, no grab
            if (!_wasTriggerDownLastFrame) return;

            //User is holding trigger, compute holding time
            if (Reticle.Triggering)
            {
                _elapsedTimeWithTriggerDown += Time.deltaTime;
            }
            //If not triggering, release
            else
            {
                _elapsedTimeWithTriggerDown = 0;
                _wasTriggerDownLastFrame = false;
                ComponentReleased(this, null);
                if(grab.IsGrabbed) grab.Release();
            }

            //If trigger held and component not yet grabbed, and wait time reached, grab component
            if (_wasTriggerDownLastFrame && !grab.IsGrabbed && _elapsedTimeWithTriggerDown >= _waitTimeBeforeGrab)
            {
                //Store component being grabbed
                CurrentlyGrabbedComponent = Reticle.CurrentRaycastResult.gameObject;
                //grab component
                grab.Grab();
            }
        }

        protected override void AfterUpdate()
        {
            base.AfterUpdate();

            //if no component grabbed, nothing to update
            if (!CurrentlyGrabbedComponent) return;
            
            //Move grabbed component around user
            CurrentlyGrabbedComponent.transform.RotateAround(Reticle.PointerCamera.transform.position, Reticle.PointerCamera.transform.up, Rotation.RotationDelta.eulerAngles.y);
            CurrentlyGrabbedComponent.transform.RotateAround(Reticle.PointerCamera.transform.position, Reticle.PointerCamera.transform.right, Rotation.RotationDelta.eulerAngles.x);
        }
    }
}
