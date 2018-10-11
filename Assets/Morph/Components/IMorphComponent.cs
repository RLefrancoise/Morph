namespace Morph.Components
{
    /// <summary>
    /// Base interface for any Morph component
    /// </summary>
    public interface IMorphComponent
    {
        /// <summary>
        /// Accept a visitor to visit the component
        /// </summary>
        /// <param name="visitor">Visitor to accept</param>
        void Accept(IMorphComponentVisitor visitor);
    }
}
