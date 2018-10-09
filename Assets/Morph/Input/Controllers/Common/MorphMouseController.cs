using Morph.Components;
using Morph.Components.Interaction;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Select;
using Morph.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="MorphAbstractController"/>
    /// <summary>
    /// Mouse controller
    /// </summary>
    public class MorphMouseController : MorphAbstractController
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainCamera == null) return;

            transform.position = MorphMain.Instance.Application.MainCamera.Camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
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
            }
        }
    }
}
