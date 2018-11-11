using Morph.Components;
using Morph.Components.Interaction;
using Morph.Core;
using Morph.Input.Controllers.Features;
using UnityEngine;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="IMorphController"/>
    /// <summary>
    /// Abstract Morph controller. You can derive it, as it implements basic stuff that any Unity controller should have 
    /// </summary>
    public abstract class MorphAbstractController : MonoBehaviour, IMorphController
    {
        #region Fields

        private Vector3? _previousPosition;
        private Vector3? _previousRotation;

        #endregion

        #region IMorphController Implementation

        public abstract MorphControllerFeatures SupportedFeatures { get; }
        public MorphFeaturePositionTracking Position { get; private set; }
        public MorphFeatureRotationTracking Rotation { get; private set; }
        public MorphFeatureTouchPad TouchPad { get; private set; }
        public MorphFeatureButtons Buttons { get; private set; }
        public MorphFeatureGestures Gestures { get; private set; }
        public MorphFeatureHaptics Haptics { get; private set; }
        public bool IsInitialized { get; private set; }

        public virtual bool Initialize()
        {
            //Listen for controller destruction to destroy gameObject
            MorphMain.Instance.Application.WhenControllerDestroyed += ControllerDestroyed;

            //Listen components registration
            MorphMain.Instance.Application.WhenComponentRegistered += ComponentRegistered;
            MorphMain.Instance.Application.WhenComponentUnregistered += ComponentUnregistered;

            IsInitialized = true;

            return true;
        }

        public bool DeInitialize()
        {
            MorphMain.Instance.Application.WhenControllerDestroyed -= ControllerDestroyed;

            MorphMain.Instance.Application.WhenComponentRegistered -= ComponentRegistered;
            MorphMain.Instance.Application.WhenComponentUnregistered -= ComponentUnregistered;

            return false;
        }

        public bool HasFeatures(MorphControllerFeatures features)
        {
            return SupportedFeatures.HasFlag(features);
        }

        #endregion

        #region Callbacks

        private void ControllerDestroyed(object sender, IMorphController controller)
        {
            if (controller as MorphAbstractController == this)
            {
                Destroy(gameObject);
            }
        }

        private void ComponentRegistered(object sender, IMorphComponent component)
        {
            WhenComponentRegistered(component);
        }

        private void ComponentUnregistered(object sender, IMorphComponent component)
        {
            WhenComponentUnregistered(component);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called when component has been registered. Override it to do custom action when it happens.
        /// </summary>
        /// <param name="component">registered component</param>
        protected virtual void WhenComponentRegistered(IMorphComponent component) { }

        /// <summary>
        /// Called when component has been unregistered. Override it to do custom action when it happens.
        /// </summary>
        /// <param name="component">unregistered component</param>
        protected virtual void WhenComponentUnregistered(IMorphComponent component) { }

        /// <summary>
        /// Called before updating the controller. Override it to do custom actions before update
        /// </summary>
        protected virtual void BeforeUpdate() { }

        /// <summary>
        /// Called after updating the controller. Override it to do custom actions after update
        /// </summary>
        protected virtual void AfterUpdate() { }

        protected void UpdatePosition()
        {
            Position.Position = transform.position;

            if (_previousPosition.HasValue)
            {
                Vector3 delta = Position.Position - _previousPosition.Value;
                Position.MovementDirection = delta.normalized;
                Position.PositionDelta = delta;
                Position.Speed = delta / Time.deltaTime;
            }

            _previousPosition = Position.Position;
        }

        protected void UpdateRotation()
        {
            Rotation.Rotation = transform.rotation;
            Rotation.ForwardDirection = transform.forward;
            Rotation.UpDirection = transform.up;
            Rotation.RightDirection = transform.right;

            if (_previousRotation.HasValue)
            {
                Vector3 delta = Rotation.Rotation.eulerAngles - _previousRotation.Value;
                Rotation.RotationDelta = delta;
                Rotation.AngularSpeed = delta / Time.deltaTime;
            }

            _previousRotation = Rotation.Rotation.eulerAngles;
        }

        protected virtual void UpdateTouchPad() { }
        protected virtual void UpdateButtons() { }
        protected virtual void UpdateGestures() { }
        protected virtual void UpdateHaptics() { }

        #endregion

        #region MonoBehaviour

        protected virtual void Awake()
        {
            Position = new MorphFeaturePositionTracking();
            Rotation = new MorphFeatureRotationTracking();
            TouchPad = new MorphFeatureTouchPad();
            Buttons = new MorphFeatureButtons();
            Gestures = new MorphFeatureGestures();
            Haptics = new MorphFeatureHaptics();
        }

        protected void Update()
        {
            if (!IsInitialized) return;

            BeforeUpdate();

            if(HasFeatures(MorphControllerFeatures.PositionTracking)) UpdatePosition();
            if(HasFeatures(MorphControllerFeatures.RotationTracking)) UpdateRotation();
            if(HasFeatures(MorphControllerFeatures.TouchPad)) UpdateTouchPad();
            if(HasFeatures(MorphControllerFeatures.Buttons)) UpdateButtons();
            if(HasFeatures(MorphControllerFeatures.Gestures)) UpdateGestures();
            if(HasFeatures(MorphControllerFeatures.Haptics)) UpdateHaptics();

            AfterUpdate();
        }

        #endregion

        #region IMorphComponentInteractionVisitor

        public virtual void Visit(IMorphComponentFocus focus) { }
        public virtual void Visit(IMorphComponentSelect select) { }
        public virtual void Visit(IMorphComponentGrab grab) { }

        #endregion
    }
}
