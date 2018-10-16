using System;
using Morph.Input.Controllers.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Input.Controllers.GoogleVR
{
    public class MorphGoogleVrReticleController : MorphControllerWithEventTrigger
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;
        protected override Ray GrabbedRay => new Ray(transform.position, transform.forward);
        protected override Predicate<BaseEventData> SelectValidation => eventData => true;
        protected override Predicate<BaseEventData> DeselectValidation => eventData => true;
    }
}
