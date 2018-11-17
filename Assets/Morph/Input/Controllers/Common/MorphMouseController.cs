using System;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Core;
using Morph.Input.Controllers.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="MorphControllerWithEventTrigger{TMorphComponentFocusWithEventTrigger, TMorphComponentSelectWithEventTrigger, TMorphComponentGrabWithEventTrigger}"/>
    /// <summary>
    /// Mouse controller
    /// </summary>
    public class MorphMouseController : MorphControllerWithEventTrigger<MorphComponentFocusWithEventTrigger, MorphComponentSelectWithEventTrigger, MorphComponentGrabWithEventTrigger>
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainDisplay == null) return;

            transform.position =
                MorphMain.Instance.Application.MainDisplay.Camera.ScreenToWorldPoint(
                    new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, MorphMain.Instance.Application.MainDisplay.Camera.nearClipPlane));
            transform.rotation = Quaternion.identity;
        }

        protected override Ray GrabbedRay => MorphMain.Instance.Application.MainDisplay.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        protected override Predicate<BaseEventData> SelectValidation => eventData => UnityEngine.Input.GetMouseButtonDown(0);
        protected override Predicate<BaseEventData> DeselectValidation => eventData => UnityEngine.Input.GetMouseButtonUp(0);
        protected override Predicate<BaseEventData> GrabValidation => eventData => UnityEngine.Input.GetMouseButton(0);
        protected override Predicate<BaseEventData> ReleaseValidation => eventData => !UnityEngine.Input.GetMouseButton(0);
    }
}
