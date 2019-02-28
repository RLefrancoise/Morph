using Morph.Components;
using Morph.Components.Interaction;
using Morph.Components.Navigation;
using Morph.Core;
using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Position;
using Morph.Input.Controllers.Features.Rotation;
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

        protected MorphDefaultFeaturePosition _position;
        protected MorphDefaultFeatureRotation _rotation;

        #endregion

        #region IMorphController Implementation

        public abstract MorphControllerFeatures SupportedFeatures { get; }
        public MorphFeaturePosition Position => _position;
        public MorphFeatureRotation Rotation => _rotation;
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
            _position.Update(transform);
        }

        protected virtual void UpdateRotation()
        {
            _rotation.Update(transform);
        }

        protected virtual void UpdateTouchPad() { }
        protected virtual void UpdateButtons() { }
        protected virtual void UpdateGestures() { }
        protected virtual void UpdateHaptics() { }

        #endregion

        #region MonoBehaviour

        protected virtual void Awake()
        {
            _position = new MorphDefaultFeaturePosition();
            _rotation = new MorphDefaultFeatureRotation();
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

        #region IMorphComponentVisitor
        
        public virtual void Visit(IMorphComponent component) { }

        public virtual void Visit(IMorphInteractiveComponent interactiveComponent) { }

        public virtual void Visit(IMorphComponentInteraction interaction) { }

        public virtual void Visit(IMorphWarpZone warpZone) { }
        
        #endregion
    }
}
