namespace Morph.Components.Interaction
{
    /// <summary>
    /// Base interface for Morph component interaction
    /// </summary>
    public interface IMorphComponentInteraction
    {
        /// <summary>
        /// Interactive component this interaction is attached to
        /// </summary>
        IMorphInteractiveComponent InteractiveComponent { get; }
    }
}
