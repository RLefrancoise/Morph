using UnityEngine;

namespace Morph.Components.Navigation
{
    /// <inheritdoc cref="IMorphPlayerController"/>
    /// <inheritdoc cref="MorphComponent"/>
    /// <summary>
    /// Morph player controller
    /// </summary>
    public class MorphPlayerController : MorphComponent, IMorphPlayerController
    {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private Camera playerCamera;
        
        /// <inheritdoc />
        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public Transform PlayerTransform => playerTransform;

        /// <inheritdoc />
        public Camera PlayerCamera => playerCamera;
    }
}