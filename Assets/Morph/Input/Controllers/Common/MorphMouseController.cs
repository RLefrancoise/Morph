using System;
using Morph.Core;
using Morph.Input.Controllers.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="MorphControllerWithEventTrigger"/>
    /// <summary>
    /// Mouse controller
    /// </summary>
    public class MorphMouseController : MorphControllerWithEventTrigger
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
    }
}
