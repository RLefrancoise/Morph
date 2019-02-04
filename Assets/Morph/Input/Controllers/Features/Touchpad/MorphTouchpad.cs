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

        private float _horizontalAxisValue;
        private float _verticalAxisValue;
        private float _horizontalAxisDelta;
        private float _verticalAxisDelta;

        public bool Clicked
        {
            get { return _clicked; }
            set
            {
                _clicked = value;
                TouchpadClicked?.Invoke(this, value);
            }
        }

        public float HorizontalAxisValue
        {
            get { return _horizontalAxisValue; }
            set
            {
                _horizontalAxisValue = value;
                HorizontalAxisValueChanged?.Invoke(this, value);
            }
        }

        public float VerticalAxisValue
        {
            get { return _verticalAxisValue; }
            set
            {
                _verticalAxisValue = value;
                VerticalAxisValueChanged?.Invoke(this, value);
            }
        }

        public float HorizontalAxisDelta
        {
            get { return _horizontalAxisDelta; }
            set
            {
                _horizontalAxisDelta = value;
                HorizontalAxisDeltaChanged?.Invoke(this, value);
            }
        }

        public float VerticalAxisDelta
        {
            get { return _verticalAxisDelta; }
            set
            {
                _verticalAxisDelta = value;
                VerticalAxisDeltaChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool> TouchpadClicked;
        public event EventHandler<float> HorizontalAxisValueChanged;
        public event EventHandler<float> VerticalAxisValueChanged;
        public event EventHandler<float> HorizontalAxisDeltaChanged;
        public event EventHandler<float> VerticalAxisDeltaChanged;
    }
}
