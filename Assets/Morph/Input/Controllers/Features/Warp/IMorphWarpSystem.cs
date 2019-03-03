namespace Morph.Input.Controllers.Features.Warp
{
    /// <summary>
    /// Interface for Morph warp system
    /// </summary>
    public interface IMorphWarpSystem
    {
        /// <summary>
        /// Guidance of the warp system
        /// </summary>
        MorphWarpSystemGuidance Guidance { get; set; }
    }
}