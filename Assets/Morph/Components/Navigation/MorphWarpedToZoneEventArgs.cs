using System;
using UnityEngine;

namespace Morph.Components.Navigation
{
    /// <summary>
    /// Morph Warp Zone Arguments for OnWarpedToZone event
    /// </summary>
    public class MorphWarpedToZoneEventArgs : EventArgs
    {
        /// <summary>
        /// Player that warped to the zone
        /// </summary>
        public IMorphPlayerController PlayerController { get; }
        /// <summary>
        /// Position in world where the player warped
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// Create a new MorphWarpedToZoneEventArgs instance
        /// </summary>
        /// <param name="playerController">Player that warped to the zone</param>
        /// <param name="position">Position in world where the player warped</param>
        public MorphWarpedToZoneEventArgs(IMorphPlayerController playerController, Vector3 position)
        {
            PlayerController = playerController;
            Position = position;
        }
    }
}