using System;

namespace Morph.Input.Controllers.Features.Touchpad
{
    /// <inheritdoc />
    /// <summary>
    /// Touchpad data
    /// </summary>
    public class MorphTouchpad : IMorphTouchpad
    {
        /// <summary>
        /// Is touchpad clicked
        /// </summary>
        private bool _clicked;

        public bool Clicked
        {
            get { return _clicked; }
            set
            {
                _clicked = value;
                TouchpadClicked?.Invoke(this, value);
            }
        }

        public event EventHandler<bool> TouchpadClicked;

        public float HorizontalAxisValue { get; set; }
        public float VerticalAxisValue { get; set; }
        public float HorizontalAxisDelta { get; set; }
        public float VerticalAxisDelta { get; set; }
    }
}
