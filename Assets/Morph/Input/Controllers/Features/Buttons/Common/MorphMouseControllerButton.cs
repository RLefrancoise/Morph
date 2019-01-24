namespace Morph.Input.Controllers.Features.Buttons.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Morph mouse controller button
    /// </summary>
    public class MorphMouseControllerButton : MorphControllerButton
    {
        public MorphMouseControllerButton(string buttonName) : base(buttonName)
        {
        }

        /// <summary>
        /// Set mouse button pressed
        /// </summary>
        /// <param name="pressed"></param>
        internal void SetPressed(bool pressed)
        {
            Pressed = pressed;
        }
    }
}
