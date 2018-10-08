using UnityEngine;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="IMorphComponentInteraction"/>
    /// <summary>
    /// Abstract Morph component interaction. You should derive this class to implement a new interaction.
    /// </summary>
    [RequireComponent(typeof(MorphInteractiveComponent))]
    public abstract class MorphComponentInteraction : MorphComponent, IMorphComponentInteraction
    {
        private MorphInteractiveComponent _interactiveComponent;

        public IMorphInteractiveComponent InteractiveComponent => _interactiveComponent;

        protected override void Awake()
        {
            base.Awake();
            _interactiveComponent = GetComponent<MorphInteractiveComponent>();
        }
    }
}
