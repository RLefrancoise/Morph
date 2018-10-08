using System;
using Morph.Input.Controllers.Features;

namespace Morph.Input.Controllers
{
    /// <summary>
    /// Base interface for Morph controllers
    /// </summary>
    public interface IMorphController
    {
        /// <summary>
        /// Features available for the controller
        /// </summary>
        MorphControllerFeatures SupportedFeatures { get; }

        /// <summary>
        /// Position
        /// </summary>
        MorphFeaturePositionTracking Position { get; }

        /// <summary>
        /// Rotation
        /// </summary>
        MorphFeatureRotationTracking Rotation { get; }

        /// <summary>
        /// Is controller initialized ?
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initialize the controller
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Deinitialize the controller
        /// </summary>
        /// <returns></returns>
        bool DeInitialize();

        /// <summary>
        /// Does the controller has specified features ?
        /// </summary>
        /// <param name="features">Features to test</param>
        /// <returns>true if controller has specified features</returns>
        bool HasFeatures(MorphControllerFeatures features);
    }
}
