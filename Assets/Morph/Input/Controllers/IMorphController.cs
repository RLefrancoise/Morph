using Morph.Components;
using Morph.Components.Interaction;
using Morph.Input.Controllers.Features;

namespace Morph.Input.Controllers
{
    /// <inheritdoc cref="IMorphComponentVisitor"/>
    /// <inheritdoc cref="IMorphComponentInteractionVisitor"/>
    /// <summary>
    /// Base interface for Morph controllers
    /// </summary>
    public interface IMorphController : IMorphComponentVisitor, IMorphComponentInteractionVisitor
    {
        /// <summary>
        /// Features available for the controller
        /// </summary>
        MorphControllerFeatures SupportedFeatures { get; }

        #region Features

        /// <summary>
        /// Position
        /// </summary>
        MorphFeaturePosition Position { get; }

        /// <summary>
        /// Rotation
        /// </summary>
        MorphFeatureRotation Rotation { get; }

        /// <summary>
        /// Trackpad
        /// </summary>
        MorphFeatureTouchpads Touchpads { get; }

        /// <summary>
        /// Buttons
        /// </summary>
        MorphFeatureButtons Buttons { get; }

        /// <summary>
        /// Gestures
        /// </summary>
        MorphFeatureGestures Gestures { get; }

        /// <summary>
        /// Haptics
        /// </summary>
        MorphFeatureHaptics Haptics { get; }
        
        /// <summary>
        /// Warp
        /// </summary>
        MorphFeatureWarp Warp { get; }

        #endregion

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
