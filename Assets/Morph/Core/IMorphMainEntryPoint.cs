namespace Morph.Core
{
    /// <summary>
    /// Interface for any Morph entry point
    /// </summary>
    public interface IMorphMainEntryPoint
    {
        /// <summary>
        /// The current application
        /// </summary>
        IMorphApplication Application { get; }
    }
}
