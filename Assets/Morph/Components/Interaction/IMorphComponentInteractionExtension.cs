namespace Morph.Components.Interaction
{
    /// <summary>
    /// Base interface for interaction extension.
    /// </summary>
    public interface IMorphComponentInteractionExtension<out TMorphComponentInteraction> where TMorphComponentInteraction : IMorphComponentInteraction
    {
        /// <summary>
        /// Morph interaction
        /// </summary>
        TMorphComponentInteraction Interaction { get; }
    }
}
