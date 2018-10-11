namespace Morph.Components.Interaction
{
    /// <summary>
    /// Visitor for Morph component interaction
    /// </summary>
    public interface IMorphComponentInteractionVisitor
    {
        void Visit(IMorphComponentFocus focus);
        void Visit(IMorphComponentSelect select);
        void Visit(IMorphComponentGrab grab);
    }
}
