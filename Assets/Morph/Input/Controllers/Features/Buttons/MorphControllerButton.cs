using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller button
    /// </summary>
    public class MorphControllerButton : MorphControllerAbstractButton
    {
        /// <summary>
        /// Is button pressed
        /// </summary>
        private bool _pressed;

        /// <summary>
        /// Is button pressed
        /// </summary>
        public bool Pressed
        {
            get { return _pressed; }
            internal set
            {
                _pressed = value;
                ButtonPressed?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Button pressed event
        /// </summary>
        public event EventHandler<bool> ButtonPressed;

        public MorphControllerButton(string buttonName) : base(buttonName)
        {
        }
    }
}
