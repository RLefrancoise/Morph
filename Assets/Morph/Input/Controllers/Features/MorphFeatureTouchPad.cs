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
            internal set
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
        public float HorizontalAxisValue { get; internal set; }
        /// <summary>
        /// Value of vertical axis between -1 and 1
        /// </summary>
        public float VerticalAxisValue { get; internal set; }

        public MorphTouchPadData(float horizontalAxisValue, float verticalAxisValue)
        {
            HorizontalAxisValue = horizontalAxisValue;
            VerticalAxisValue = verticalAxisValue;
        }
    }
}
