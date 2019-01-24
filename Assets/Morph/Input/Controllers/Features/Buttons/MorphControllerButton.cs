using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller button
    /// </summary>
    public abstract class MorphControllerButton : MorphControllerAbstractButton
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
            protected set
            {
                _pressed = value;
                ButtonPressed?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Button pressed event
        /// </summary>
        public event EventHandler<bool> ButtonPressed;

        protected MorphControllerButton(string buttonName) : base(buttonName)
        {
        }
    }
}
