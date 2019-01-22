using System;
using Morph.Components.Interaction;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Input.Controllers.Features;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Abstract morph controller for controllers that use Event Trigger
    /// <typeparam name="TMorphComponentFocusWithEventTrigger">Focus component to use</typeparam>
    /// <typeparam name="TMorphComponentSelectWithEventTrigger">Select component to use</typeparam>
    /// <typeparam name="TMorphComponentGrabWithEventTrigger">Grab component to use</typeparam>
    /// </summary>
    public abstract class MorphControllerWithEventTrigger<TMorphComponentFocusWithEventTrigger, TMorphComponentSelectWithEventTrigger, TMorphComponentGrabWithEventTrigger> : MorphAbstractController where TMorphComponentFocusWithEventTrigger : MorphComponentFocusWithEventTrigger where TMorphComponentSelectWithEventTrigger : MorphComponentSelectWithEventTrigger where TMorphComponentGrabWithEventTrigger : MorphComponentGrabWithEventTrigger
    {
        public override MorphFeatureButtons Buttons => new MorphFeatureButtons();

        /// <summary>
        /// Currently grabbed object
        /// </summary>
        protected GameObject CurrentlyGrabbedComponent;
        /// <summary>
        /// Position of grabbed object hit point at last frame
        /// </summary>
        protected Vector3? PreviousGrabbedComponentHitPoint;

        /// <summary>
        /// Ray to use while grabbing to update PreviousGrabbedComponentHitPoint and move the object
        /// </summary>
        protected abstract Ray GrabbedRay { get; }

        /// <summary>
        /// Predicate to be true to validate selection
        /// </summary>
        protected abstract Predicate<BaseEventData> SelectValidation { get; }
        
        /// <summary>
        /// Predicate to be true to validate deselection
        /// </summary>
        protected abstract Predicate<BaseEventData> DeselectValidation { get; }

        /// <summary>
        /// Predicate to be true to validate grab
        /// </summary>
        protected abstract Predicate<BaseEventData> GrabValidation { get; }

        /// <summary>
        /// Predicate to be true to validate release
        /// </summary>
        protected abstract Predicate<BaseEventData> ReleaseValidation { get; }

        /// <summary>
        /// Callback to use while grabbing
        /// </summary>
        protected virtual Action<BaseEventData> WhileGrabbed => (eventData) =>
        {
            Ray ray = GrabbedRay;
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.collider.gameObject != CurrentlyGrabbedComponent) return;

            if (PreviousGrabbedComponentHitPoint.HasValue)
            {
                Vector3 delta = hit.point - PreviousGrabbedComponentHitPoint.Value;
                delta.z = 0f;
                CurrentlyGrabbedComponent.transform.position += delta;
            }

            PreviousGrabbedComponentHitPoint = hit.point;
        };

        #region IMorphComponentInteractionVisitor
        public override void Visit(IMorphComponentFocus visitable)
        {
            base.Visit(visitable);

            GameObject componentGameObject = (visitable as Component)?.gameObject;
            if (!componentGameObject) return;

            TMorphComponentFocusWithEventTrigger focusWithEventTrigger = componentGameObject.GetComponent<TMorphComponentFocusWithEventTrigger>();
            if (!focusWithEventTrigger) componentGameObject.AddComponent<TMorphComponentFocusWithEventTrigger>();
        }

        public override void Visit(IMorphComponentSelect visitable)
        {
            base.Visit(visitable);

            GameObject componentGameObject = (visitable as Component)?.gameObject;
            if (!componentGameObject) return;

            TMorphComponentSelectWithEventTrigger selectWithEventTrigger = componentGameObject.GetComponent<TMorphComponentSelectWithEventTrigger>();
            if (!selectWithEventTrigger) selectWithEventTrigger = componentGameObject.AddComponent<TMorphComponentSelectWithEventTrigger>();

            selectWithEventTrigger.SelectValidation = SelectValidation;
            selectWithEventTrigger.DeselectValidation = DeselectValidation;
        }

        public override void Visit(IMorphComponentGrab visitable)
        {
            base.Visit(visitable);

            GameObject componentGameObject = (visitable as Component)?.gameObject;
            if (!componentGameObject) return;

            TMorphComponentGrabWithEventTrigger grabWithEventTrigger = componentGameObject.GetComponent<TMorphComponentGrabWithEventTrigger>();
            if (!grabWithEventTrigger) grabWithEventTrigger = componentGameObject.AddComponent<TMorphComponentGrabWithEventTrigger>();

            grabWithEventTrigger.GrabValidation += GrabValidation;
            grabWithEventTrigger.ReleaseValidation += ReleaseValidation;

            grabWithEventTrigger.Grab.Grabbed += ComponentGrabbed;
            grabWithEventTrigger.Grab.Released += ComponentReleased;
            grabWithEventTrigger.WhileGrabbed += WhileGrabbed;
        }

        #endregion

        protected virtual void ComponentReleased(object sender, EventArgs e)
        {
            CurrentlyGrabbedComponent = null;
            PreviousGrabbedComponentHitPoint = null;
        }

        protected virtual void ComponentGrabbed(object sender, EventArgs e)
        {
            CurrentlyGrabbedComponent = (sender as Component).gameObject;
        }
    }
}
