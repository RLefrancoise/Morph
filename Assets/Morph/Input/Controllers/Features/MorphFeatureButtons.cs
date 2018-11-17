using Morph.Input.Controllers.Features.Buttons;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller buttons feature
    /// </summary>
    public class MorphFeatureButtons : IMorphControllerFeature
    {
        /// <summary>
        /// Buttons
        /// </summary>
        public MorphControllerButton[] Buttons { get; internal set; }

        /// <summary>
        /// Triggers
        /// </summary>
        public MorphControllerTriggerButton[] Triggers { get; internal set; }
    }
}
