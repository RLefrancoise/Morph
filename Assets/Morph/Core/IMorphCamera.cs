using UnityEngine;

namespace Morph.Core
{
    /// <summary>
    /// Interface for any camera in morph application
    /// </summary>
    public interface IMorphCamera
    {
        /// <summary>
        /// Unity camera
        /// </summary>
        Camera Camera { get; }
    }
}
