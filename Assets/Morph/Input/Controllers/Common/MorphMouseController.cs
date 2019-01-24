using System;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Core;
using Morph.Input.Controllers.Common;
using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Buttons;
using Morph.Input.Controllers.Features.Buttons.Common;
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
        private MorphMouseControllerButton _leftButton;
        private MorphMouseControllerButton _rightButton;
        private MorphMouseControllerButton _middleButton;
        private MorphFeatureButtons _buttons;

        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking | MorphControllerFeatures.Buttons;

        public override MorphFeatureButtons Buttons => _buttons;

        public override bool Initialize()
        {
            if (!base.Initialize())
                return false;

            _leftButton = new MorphMouseControllerButton("Left button");
            _rightButton = new MorphMouseControllerButton("Right button");
            _middleButton = new MorphMouseControllerButton("Middle button");

            _buttons = new MorphFeatureButtons(new MorphControllerButton[]
            {
                _leftButton,
                _rightButton,
                _middleButton
            }, new MorphControllerTriggerButton[0]);

            return true;
        }

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainDisplay == null) return;

            transform.position =
                MorphMain.Instance.Application.MainDisplay.Camera.ScreenToWorldPoint(
                    new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, MorphMain.Instance.Application.MainDisplay.Camera.nearClipPlane));
            transform.rotation = Quaternion.identity;
        }

        protected override void UpdateButtons()
        {
            base.UpdateButtons();

            _leftButton.SetPressed(UnityEngine.Input.GetMouseButton(0));
            _rightButton.SetPressed(UnityEngine.Input.GetMouseButton(1));
            _middleButton.SetPressed(UnityEngine.Input.GetMouseButton(2));
        }

        protected override Ray GrabbedRay => MorphMain.Instance.Application.MainDisplay.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        protected override Predicate<BaseEventData> SelectValidation => eventData => UnityEngine.Input.GetMouseButtonDown(0);
        protected override Predicate<BaseEventData> DeselectValidation => eventData => UnityEngine.Input.GetMouseButtonUp(0);
        protected override Predicate<BaseEventData> GrabValidation => eventData => UnityEngine.Input.GetMouseButton(0);
        protected override Predicate<BaseEventData> ReleaseValidation => eventData => !UnityEngine.Input.GetMouseButton(0);
    }
}
