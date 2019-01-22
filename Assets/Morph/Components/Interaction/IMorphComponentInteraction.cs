namespace Morph.Components.Interaction
{
    /// <inheritdoc />
    /// <summary>
    /// Base interface for Morph component interaction
    /// </summary>
    public interface IMorphComponentInteraction : IMorphComponent
    {
        /// <summary>
        /// Interactive component this interaction is attached to
        /// </summary>
        IMorphInteractiveComponent InteractiveComponent { get; }

        /// <summary>
        /// Accept a component interaction visitor to visit us
        /// </summary>
        /// <param name="visitor">Visitor</param>
        void Accept(IMorphComponentInteractionVisitor visitor);
    }
}
