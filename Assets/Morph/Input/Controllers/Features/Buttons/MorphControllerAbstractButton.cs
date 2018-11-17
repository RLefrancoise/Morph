namespace Morph.Input.Controllers.Features.Buttons
{
    /// <summary>
    /// Base class for controller buttons
    /// </summary>
    public abstract class MorphControllerAbstractButton
    {
        /// <summary>
        /// Name of the button
        /// </summary>
        public string ButtonName { get; }

        protected MorphControllerAbstractButton(string buttonName)
        {
            ButtonName = buttonName;
        }
    }
}
