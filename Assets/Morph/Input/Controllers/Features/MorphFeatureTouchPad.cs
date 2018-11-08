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
        /// Value of horizontal axis between 0 and 1
        /// </summary>
        public float HorizontalAxisValue { get; internal set; }
        /// <summary>
        /// Value of vertical axis between 0 and 1
        /// </summary>
        public float VerticalAxisValue { get; internal set; }

        public MorphTouchPadData(float horizontalAxisValue, float verticalAxisValue)
        {
            HorizontalAxisValue = horizontalAxisValue;
            VerticalAxisValue = verticalAxisValue;
        }
    }
}
