using System;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Touchpad feature
    /// </summary>
    public class MorphFeatureTouchPad : IMorphControllerFeature
    {
        /// <summary>
        /// Trackpads data
        /// </summary>
        public MorphTouchPadData[] TouchPads { get; set; } 
    }

    /// <summary>
    /// Touchpad data
    /// </summary>
    public class MorphTouchPadData
    {
        /// <summary>
        /// Is touchpad clicked
        /// </summary>
        private bool _clicked;

        /// <summary>
        /// Is touchpad clicked
        /// </summary>
        public bool Clicked
        {
            get { return _clicked; }
            set
            {
                _clicked = value;
                TouchpadClicked?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Touchpad clicked event
        /// </summary>
        public event EventHandler<bool> TouchpadClicked;

        /// <summary>
        /// Value of horizontal axis between -1 and 1
        /// </summary>
        public float HorizontalAxisValue { get; set; }
        /// <summary>
        /// Value of vertical axis between -1 and 1
        /// </summary>
        public float VerticalAxisValue { get; set; }

        /// <summary>
        /// Delta of horizontal axis between current value and previous value
        /// </summary>
        public float HorizontalAxisDelta { get; set; }

        /// <summary>
        /// Delta of vertical axis between current value and previous value
        /// </summary>
        /// <value></value>
        public float VerticalAxisDelta { get; set; }

        public MorphTouchPadData(float horizontalAxisValue, float verticalAxisValue)
        {
            HorizontalAxisValue = horizontalAxisValue;
            VerticalAxisValue = verticalAxisValue;
        }
    }
}
