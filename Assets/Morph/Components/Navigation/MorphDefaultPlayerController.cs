using UnityEngine;

namespace Morph.Components.Navigation
{
    /// <inheritdoc cref="MorphComponent"/>
    /// <inheritdoc cref="IMorphPlayerController"/>
    /// <summary>
    /// Morph default player controller
    /// </summary>
    public class MorphDefaultPlayerController : MorphComponent, IMorphPlayerController
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