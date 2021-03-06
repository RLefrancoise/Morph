﻿using Morph.Input.Controllers.Features.Haptics;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller feature Haptics
    /// </summary>
    public class MorphFeatureHaptics : IMorphControllerFeature
    {
        /// <summary>
        /// Haptic system to use to controller vibration of the controller.
        /// </summary>
        public IMorphControllerHapticSystem HapticSystem { get; }

        public MorphFeatureHaptics(IMorphControllerHapticSystem hapticSystem)
        {
            HapticSystem = hapticSystem;
        }
    }
}
