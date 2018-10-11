using Morph.Components.Interaction;

namespace Morph.Components
{
    /// <summary>
    /// Visitor for Morph component
    /// </summary>
    public interface IMorphComponentVisitor
    {
        void Visit(IMorphComponent component);

        /// <summary>
        /// Visit an interactive component
        /// </summary>
        /// <param name="interactiveComponent"></param>
        void Visit(IMorphInteractiveComponent interactiveComponent);

        /// <summary>
        /// Visit an interaction component
        /// </summary>
        /// <param name="interaction"></param>
        void Visit(IMorphComponentInteraction interaction);
    }
}
