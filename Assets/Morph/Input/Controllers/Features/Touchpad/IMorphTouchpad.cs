﻿using System;

namespace Morph.Input.Controllers.Features.Touchpad
{
    /// <summary>
    /// Interface for Morph touchpad
    /// </summary>
    public interface IMorphTouchpad
    {
        /// <summary>
        /// Is touchpad clicked
        /// </summary>
        bool Clicked { get; }

        /// <summary>
        /// Touchpad clicked event
        /// </summary>
        event EventHandler<bool> TouchpadClicked;

        /// <summary>
        /// Value of horizontal axis between -1 and 1
        /// </summary>
        float HorizontalAxisValue { get; }
        /// <summary>
        /// Value of vertical axis between -1 and 1
        /// </summary>
        float VerticalAxisValue { get; }

        /// <summary>
        /// Delta of horizontal axis between current value and previous value
        /// </summary>
        float HorizontalAxisDelta { get; }

        /// <summary>
        /// Delta of vertical axis between current value and previous value
        /// </summary>
        float VerticalAxisDelta { get; }

        /// <summary>
        /// Horizontal axis value changed event
        /// </summary>
        event EventHandler<float> HorizontalAxisValueChanged;
        /// <summary>
        /// Vertical axis value changed event
        /// </summary>
        event EventHandler<float> VerticalAxisValueChanged;
        /// <summary>
        /// Horizontal axis delta changed
        /// </summary>
        event EventHandler<float> HorizontalAxisDeltaChanged;
        /// <summary>
        /// Vertical axis delta changed
        /// </summary>
        event EventHandler<float> VerticalAxisDeltaChanged;
    }
}
