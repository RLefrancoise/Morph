using System;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab.GoogleVR;
using Morph.Components.Interaction.Select;
using Morph.Input.Controllers.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.GoogleVR
{
    public class MorphGoogleVrReticleController : MorphControllerWithEventTrigger<MorphComponentFocusWithEventTrigger, MorphComponentSelectWithEventTrigger, MorphComponentGoogleVrReticleGrab>
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;
        protected override Ray GrabbedRay => new Ray(transform.position, transform.forward);
        protected override Predicate<BaseEventData> SelectValidation => eventData => true;
        protected override Predicate<BaseEventData> DeselectValidation => eventData => true;
    }
}
