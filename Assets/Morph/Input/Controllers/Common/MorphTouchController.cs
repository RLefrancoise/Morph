using Morph.Core;
using UnityEngine;

namespace Morph.Input.Controllers.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Morph touch controller
    /// </summary>
    public class MorphTouchController : MorphAbstractController
    {
        public override MorphControllerFeatures SupportedFeatures => MorphControllerFeatures.PositionTracking | MorphControllerFeatures.RotationTracking;

        protected override void Awake()
        {
            base.Awake();

            //Destroy if touch not supported
            if (!UnityEngine.Input.touchSupported) MorphMain.Instance.Application.DestroyController(this);
        }

        protected override void BeforeUpdate()
        {
            if (MorphMain.Instance.Application.MainCamera == null) return;

            if (UnityEngine.Input.touchCount == 0 || UnityEngine.Input.GetTouch(0).phase != TouchPhase.Began) return;

            transform.position = MorphMain.Instance.Application.MainCamera.Camera.ScreenToWorldPoint(UnityEngine.Input.GetTouch(0).position);
            transform.rotation = Quaternion.identity;
        }
    }
}
