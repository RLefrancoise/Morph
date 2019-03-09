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

        /// <summary>
        /// Max warp distance
        /// </summary>
        float MaxWarpDistance { get; set; }
        
        /// <summary>
        /// Segments number for curve guidance
        /// </summary>
        int CurveGuidanceSegments { get; set; }
        
        /// <summary>
        /// Is guidance supported by the warp system
        /// </summary>
        /// <param name="warpSystemGuidance">Guidance to test the support</param>
        /// <returns>true if supported, false otherwise</returns>
        bool IsGuidanceSupported(MorphWarpSystemGuidance warpSystemGuidance);
    }
}