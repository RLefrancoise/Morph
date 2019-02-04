﻿using Morph.Components.Interaction;
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

        protected Vector3? _previousPosition;
        protected Vector3? _previousRotation;

        #endregion

        #region IMorphController Implementation

        public abstract MorphControllerFeatures SupportedFeatures { get; }
        public MorphFeaturePosition Position { get; private set; }
        public MorphFeatureRotation Rotation { get; private set; }
        public abstract MorphFeatureTouchpads Touchpads { get; }
        public abstract MorphFeatureButtons Buttons { get; }
        public abstract MorphFeatureGestures Gestures { get; }
        public abstract MorphFeatureHaptics Haptics { get; }
        public bool IsInitialized { get; private set; }

        public virtual bool Initialize()
        {
            //Listen for controller destruction to destroy gameObject
            MorphMain.Instance.Application.WhenControllerDestroyed += ControllerDestroyed;

            IsInitialized = true;

            return true;
        }

        public bool DeInitialize()
        {
            MorphMain.Instance.Application.WhenControllerDestroyed -= ControllerDestroyed;

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

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called before updating the controller. Override it to do custom actions before update
        /// </summary>
        protected virtual void BeforeUpdate() { }

        /// <summary>
        /// Called after updating the controller. Override it to do custom actions after update
        /// </summary>
        protected virtual void AfterUpdate() { }

        protected virtual void UpdatePosition()
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

        protected virtual void UpdateRotation()
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
            Position = new MorphFeaturePosition();
            Rotation = new MorphFeatureRotation();
        }

        protected void Update()
        {
            if (!IsInitialized) return;

            BeforeUpdate();

            if(HasFeatures(MorphControllerFeatures.Position)) UpdatePosition();
            if(HasFeatures(MorphControllerFeatures.Rotation)) UpdateRotation();
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
