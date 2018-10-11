using System;
using Morph.Components.Interaction;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Abstract morph controller for controllers that use Event Trigger
    /// </summary>
    public abstract class MorphControllerWithEventTrigger : MorphAbstractController
    {
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

            MorphComponentFocusWithEventTrigger focusWithEventTrigger = componentGameObject.GetComponent<MorphComponentFocusWithEventTrigger>();
            if (!focusWithEventTrigger) componentGameObject.AddComponent<MorphComponentFocusWithEventTrigger>();
        }

        public override void Visit(IMorphComponentSelect visitable)
        {
            base.Visit(visitable);

            GameObject componentGameObject = (visitable as Component)?.gameObject;
            if (!componentGameObject) return;

            MorphComponentSelectWithEventTrigger selectWithEventTrigger = componentGameObject.GetComponent<MorphComponentSelectWithEventTrigger>();
            if (!selectWithEventTrigger) selectWithEventTrigger = componentGameObject.AddComponent<MorphComponentSelectWithEventTrigger>();

            //Select only if left mouse button down
            selectWithEventTrigger.SelectValidation = eventData => UnityEngine.Input.GetMouseButtonDown(0);
            //Deselect only if left mouse button up
            selectWithEventTrigger.DeselectValidation = eventData => UnityEngine.Input.GetMouseButtonUp(0);
        }

        public override void Visit(IMorphComponentGrab visitable)
        {
            base.Visit(visitable);

            GameObject componentGameObject = (visitable as Component)?.gameObject;
            if (!componentGameObject) return;

            MorphComponentGrabWithEventTrigger grabWithEventTrigger = componentGameObject.GetComponent<MorphComponentGrabWithEventTrigger>();
            if (!grabWithEventTrigger) grabWithEventTrigger = componentGameObject.AddComponent<MorphComponentGrabWithEventTrigger>();

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
