using Morph.Input.Controllers.Features.Warp;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph Feature Warp
    /// </summary>
    public class MorphFeatureWarp : IMorphControllerFeature
    {
        /// <summary>
        /// Warp system
        /// </summary>
        public IMorphWarpSystem WarpSystem { get; }

        public MorphFeatureWarp(IMorphWarpSystem warpSystem)
        {
            WarpSystem = warpSystem;
        }
    }
}