namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponent"/>
    /// <inheritdoc cref="IMorphComponentInteractionExtension"/>
    /// <summary>
    /// base class for interaction extension
    /// </summary>
    public abstract class MorphComponentInteractionExtension<TMorphComponentInteraction> : MorphComponent, IMorphComponentInteractionExtension<TMorphComponentInteraction> where TMorphComponentInteraction : IMorphComponentInteraction
    {
        public TMorphComponentInteraction Interaction { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Interaction = GetComponent<TMorphComponentInteraction>();
        }
    }
}
