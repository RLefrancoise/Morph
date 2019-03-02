using System;
using UnityEngine;

namespace Morph.Components.Navigation
{
    /// <inheritdoc cref="IMorphWarpZone"/>
    /// <inheritdoc cref="MorphComponent"/>
    /// <summary>
    /// Morph warp zone
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class MorphWarpZone : MorphComponent, IMorphWarpZone
    {
        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public event EventHandler<MorphWarpedToZoneEventArgs> OnWarpedToZone;
    }
}