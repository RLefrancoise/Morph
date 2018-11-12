using UnityEngine;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="IMorphComponentInteraction"/>
    /// <summary>
    /// Abstract Morph component interaction. You should derive this class to implement a new interaction.
    /// </summary>
    [RequireComponent(typeof(MorphInteractiveComponent))]
    public abstract class MorphComponentInteraction<TMorphComponentInteraction> : MorphComponent, IMorphComponentInteraction where TMorphComponentInteraction : IMorphComponentInteraction
    {
        private MorphInteractiveComponent _interactiveComponent;

        public IMorphInteractiveComponent InteractiveComponent => _interactiveComponent;

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this as IMorphComponentInteraction);
        }

        public abstract void Accept(IMorphComponentInteractionVisitor visitor);

        protected virtual void OnEnable()
        {
            MorphComponentInteractionExtension<TMorphComponentInteraction>[] extensions = GetComponents<MorphComponentInteractionExtension<TMorphComponentInteraction>>();
            foreach (var extension in extensions)
            {
                if (extension.Interaction as MorphComponentInteraction<TMorphComponentInteraction> != this) continue;
                extension.enabled = true;
            }
        }

        protected virtual void OnDisable()
        {
            MorphComponentInteractionExtension<TMorphComponentInteraction>[] extensions = GetComponents<MorphComponentInteractionExtension<TMorphComponentInteraction>>();
            foreach (var extension in extensions)
            {
                if (extension.Interaction as MorphComponentInteraction<TMorphComponentInteraction> != this) continue;
                extension.enabled = false;
            }
        }

        protected override void Start()
        {
            base.Start();
            _interactiveComponent = GetComponent<MorphInteractiveComponent>();
        }
    }
}
