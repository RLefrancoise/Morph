using Morph.Input.Controllers.Features.Touchpad;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph Touchpad feature
    /// </summary>
    public class MorphFeatureTouchpads : IMorphControllerFeature
    {
        /// <summary>
        /// Touchpads
        /// </summary>
        public IMorphTouchpad[] Touchpads { get; set; }

        public MorphFeatureTouchpads(IMorphTouchpad[] touchpads)
        {
            Touchpads = touchpads;
        }
    }
}
