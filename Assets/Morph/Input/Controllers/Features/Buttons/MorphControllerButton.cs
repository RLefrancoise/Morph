using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <inheritdoc cref="MorphControllerAbstractButton"/>
    /// <inheritdoc cref="IMorphControllerButton"/>
    /// <summary>
    /// Morph controller button
    /// </summary>
    public class MorphControllerButton : MorphControllerAbstractButton, IMorphControllerButton
    {
        /// <summary>
        /// Is button pressed
        /// </summary>
        private bool _pressed;

        public bool Pressed
        {
            get { return _pressed; }
            set
            {
                _pressed = value;
                ButtonPressed?.Invoke(this, value);
            }
        }

        public event EventHandler<bool> ButtonPressed;

        public MorphControllerButton(string buttonName) : base(buttonName)
        {
        }
    }
}
