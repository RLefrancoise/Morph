using Morph.Components;
using Morph.Components.Interaction;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Core;
using UnityEngine;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="MorphAbstractController"/>
    /// <summary>
    /// Mouse controller
    /// </summary>
    public class MorphMouseController : MorphAbstractController
    {
        private GameObject _currentlyGrabbedComponent;
        private Vector3? _previousGrabbedComponentHitPoint;

        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainCamera == null) return;

            transform.position =
                MorphMain.Instance.Application.MainCamera.Camera.ScreenToWorldPoint(
                    new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, MorphMain.Instance.Application.MainCamera.Camera.nearClipPlane));
            transform.rotation = Quaternion.identity;
        }

        protected override void WhenComponentRegistered(IMorphComponent component)
        {
            base.WhenComponentRegistered(component);

            //look if component has an event trigger
            GameObject componentGameObject = (component as Component)?.gameObject;
            if (!componentGameObject) return;
            
            //Is interaction component
            if (component is IMorphComponentInteraction)
            {
                //Focus
                if (component is IMorphComponentFocus)
                {
                    MorphComponentFocusWithEventTrigger focusWithEventTrigger = componentGameObject.GetComponent<MorphComponentFocusWithEventTrigger>();
                    if (!focusWithEventTrigger) componentGameObject.AddComponent<MorphComponentFocusWithEventTrigger>();
                }
                //Select
                else if (component is IMorphComponentSelect)
                {
                    MorphComponentSelectWithEventTrigger selectWithEventTrigger = componentGameObject.GetComponent<MorphComponentSelectWithEventTrigger>();
                    if (!selectWithEventTrigger) selectWithEventTrigger = componentGameObject.AddComponent<MorphComponentSelectWithEventTrigger>();

                    //Select only if left mouse button down
                    selectWithEventTrigger.SelectValidation = eventData => UnityEngine.Input.GetMouseButtonDown(0);
                    //Deselect only if left mouse button up
                    selectWithEventTrigger.DeselectValidation = eventData => UnityEngine.Input.GetMouseButtonUp(0);
                }
                //Grab
                else if (component is IMorphComponentGrab)
                {
                    MorphComponentGrabWithEventTrigger grabWithEventTrigger = componentGameObject.GetComponent<MorphComponentGrabWithEventTrigger>();
                    if (!grabWithEventTrigger) grabWithEventTrigger = componentGameObject.AddComponent<MorphComponentGrabWithEventTrigger>();

                    grabWithEventTrigger.Grab.Grabbed += ComponentGrabbed;
                    grabWithEventTrigger.Grab.Released += ComponentReleased;

                    grabWithEventTrigger.WhileGrabbed += (eventData) =>
                    {
                        Ray ray = MorphMain.Instance.Application.MainCamera.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.gameObject != _currentlyGrabbedComponent) return;

                            if (_previousGrabbedComponentHitPoint.HasValue)
                            {
                                Vector3 delta = hit.point - _previousGrabbedComponentHitPoint.Value;
                                delta.z = 0f;
                                _currentlyGrabbedComponent.transform.position += delta;
                            }
                                
                            _previousGrabbedComponentHitPoint = hit.point;
                        }
                    };
                }
            }
        }

        private void ComponentReleased(object sender, System.EventArgs e)
        {
            _currentlyGrabbedComponent = null;
            _previousGrabbedComponentHitPoint = null;
        }

        private void ComponentGrabbed(object sender, System.EventArgs e)
        {
            _currentlyGrabbedComponent = (sender as Component).gameObject;
        }
    }
}
