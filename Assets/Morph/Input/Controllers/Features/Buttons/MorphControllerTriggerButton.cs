using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller trigger button
    /// </summary>
    public class MorphControllerTriggerButton : MorphControllerAbstractButton
    {
        /// <summary>
        /// Value of the trigger, between 0 and 1
        /// </summary>
        private float _triggerValue;

        /// <summary>
        /// Value of the trigger, between 0 and 1
        /// </summary>
        public float TriggerValue {
            get { return _triggerValue; }
            set
            {
                _triggerValue = value;
                TriggerValueChanged?.Invoke(this, value);
            }
        }

        /// <summary>
        /// Trigger value changed event
        /// </summary>
        public event EventHandler<float> TriggerValueChanged; 

        public MorphControllerTriggerButton(string buttonName) : base(buttonName)
        {
        }
    }
}
