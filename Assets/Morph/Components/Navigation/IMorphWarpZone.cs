using System;

namespace Morph.Components.Navigation
{
    /// <inheritdoc />
    /// <summary>
    /// Interface for Morph warp zone
    /// </summary>
    public interface IMorphWarpZone : IMorphComponent
    {
        /// <summary>
        /// Called when a player warped to the zone
        /// </summary>
        event EventHandler<MorphWarpedToZoneEventArgs> OnWarpedToZone;
    }
}