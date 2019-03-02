using Morph.Components.Interaction;
using Morph.Components.Navigation;

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

        /// <summary>
        /// Visit a warp zone
        /// </summary>
        /// <param name="warpZone"></param>
        void Visit(IMorphWarpZone warpZone);

        /// <summary>
        /// Visit a player controller
        /// </summary>
        /// <param name="playerController"></param>
        void Visit(IMorphPlayerController playerController);
    }
}
