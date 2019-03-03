using System;
using Morph.Components.Interaction.Focus;
using Morph.Components.Interaction.Grab;
using Morph.Components.Interaction.Select;
using Morph.Components.Navigation;
using Morph.Components.Utils;
using Morph.Core;
using Morph.Input.Controllers.Common;
using Morph.Input.Controllers.Features;
using Morph.Input.Controllers.Features.Buttons;
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
        private MorphControllerButton _leftButton;
        private MorphControllerButton _rightButton;
        private MorphControllerButton _middleButton;
        private MorphFeatureButtons _buttons;

        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.Position | MorphControllerFeatures.Buttons | MorphControllerFeatures.Warp;

        public override MorphFeatureTouchpads Touchpads => null;
        public override MorphFeatureButtons Buttons => _buttons;
        public override MorphFeatureGestures Gestures => null; //TODO: to be implemented
        public override MorphFeatureHaptics Haptics => null;
        public override MorphFeatureWarp Warp => null;
        
        public override bool Initialize()
        {
            _leftButton = new MorphControllerButton("Left button");
            _rightButton = new MorphControllerButton("Right button");
            _middleButton = new MorphControllerButton("Middle button");

            _buttons = new MorphFeatureButtons(new IMorphControllerButton[]
            {
                _leftButton,
                _rightButton,
                _middleButton
            });

            return base.Initialize();
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

            _leftButton.Pressed = UnityEngine.Input.GetMouseButton(0);
            _rightButton.Pressed = UnityEngine.Input.GetMouseButton(1);
            _middleButton.Pressed = UnityEngine.Input.GetMouseButton(2);
        }

        public override void Visit(IMorphPlayerController playerController)
        {
            base.Visit(playerController);
            
            var player = (playerController as Component);
            if (player == null) return;
            
            var control = player.gameObject.AddComponent<MorphControlTransformWithMouse>();
            control.TransformToControl = player.transform;
        }

        protected override Ray GrabbedRay => MorphMain.Instance.Application.MainDisplay.Camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        protected override Predicate<BaseEventData> SelectValidation => eventData => UnityEngine.Input.GetMouseButtonDown(0);
        protected override Predicate<BaseEventData> DeselectValidation => eventData => UnityEngine.Input.GetMouseButtonUp(0);
        protected override Predicate<BaseEventData> GrabValidation => eventData => UnityEngine.Input.GetMouseButton(0);
        protected override Predicate<BaseEventData> ReleaseValidation => eventData => !UnityEngine.Input.GetMouseButton(0);
    }
}
