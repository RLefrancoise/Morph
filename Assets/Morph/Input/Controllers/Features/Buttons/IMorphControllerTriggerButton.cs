using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <summary>
    /// Interface for Morph controller trigger button
    /// </summary>
    public interface IMorphControllerTriggerButton
    {
        /// <summary>
        /// Button name
        /// </summary>
        string ButtonName { get; }
        /// <summary>
        /// Value of the trigger, between 0 and 1
        /// </summary>
        float TriggerValue { get; }

        /// <summary>
        /// Trigger value changed event
        /// </summary>
        event EventHandler<float> TriggerValueChanged;
    }
}
