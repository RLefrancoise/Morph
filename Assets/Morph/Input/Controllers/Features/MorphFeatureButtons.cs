using Morph.Input.Controllers.Features.Buttons;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller buttons feature
    /// </summary>
    public class MorphFeatureButtons : IMorphControllerFeature
    {
        public MorphFeatureButtons()
        {
            Buttons = new IMorphControllerButton[0];
            Triggers = new IMorphControllerTriggerButton[0];
        }

        public MorphFeatureButtons(IMorphControllerButton[] buttons, IMorphControllerTriggerButton[] triggers)
        {
            Buttons = buttons;
            Triggers = triggers;
        }

        /// <summary>
        /// Buttons
        /// </summary>
        public IMorphControllerButton[] Buttons { get; }

        /// <summary>
        /// Triggers
        /// </summary>
        public IMorphControllerTriggerButton[] Triggers { get; }
    }
}
