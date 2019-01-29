using System;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Core;
using Morph.Input.Controllers.Features;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Morph touch controller
    /// </summary>
    public class MorphTouchController : MorphControllerWithEventTrigger<MorphComponentFocusWithEventTrigger, MorphComponentSelectWithEventTrigger, MorphComponentGrabWithEventTrigger>
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.Position;
        public override MorphFeatureButtons Buttons => null;

        protected override Ray GrabbedRay => MorphMain.Instance.Application.MainDisplay.Camera.ScreenPointToRay(UnityEngine.Input.GetTouch(0).position);

        protected override Predicate<BaseEventData> SelectValidation => eventData => UnityEngine.Input.touchCount > 0;
        protected override Predicate<BaseEventData> DeselectValidation => eventData => UnityEngine.Input.touchCount == 0;
        protected override Predicate<BaseEventData> GrabValidation => SelectValidation;
        protected override Predicate<BaseEventData> ReleaseValidation => DeselectValidation;

        protected void Start()
        {
            //Destroy if touch not supported
            if (!UnityEngine.Input.touchSupported) MorphMain.Instance.Application.DestroyController(this);
        }

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainDisplay == null) return;

            if (UnityEngine.Input.touchCount == 0 || UnityEngine.Input.GetTouch(0).phase != TouchPhase.Began) return;

            transform.position = MorphMain.Instance.Application.MainDisplay.Camera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
            transform.rotation = Quaternion.identity;
        }
    }
}
