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
            Buttons = new MorphControllerButton[0];
            Triggers = new MorphControllerTriggerButton[0];
        }

        public MorphFeatureButtons(MorphControllerButton[] buttons, MorphControllerTriggerButton[] triggers)
        {
            Buttons = buttons;
            Triggers = triggers;
        }

        /// <summary>
        /// Buttons
        /// </summary>
        public MorphControllerButton[] Buttons { get; }

        /// <summary>
        /// Triggers
        /// </summary>
        public MorphControllerTriggerButton[] Triggers { get; }
    }
}
