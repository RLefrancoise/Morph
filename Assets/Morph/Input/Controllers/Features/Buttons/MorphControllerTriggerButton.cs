using System;

namespace Morph.Input.Controllers.Features.Buttons
{
    /// <inheritdoc cref="MorphControllerAbstractButton"/>
    /// <inheritdoc cref="IMorphControllerTriggerButton"/>
    /// <summary>
    /// Morph controller trigger button
    /// </summary>
    public class MorphControllerTriggerButton : MorphControllerAbstractButton, IMorphControllerTriggerButton
    {
        /// <summary>
        /// Value of the trigger, between 0 and 1
        /// </summary>
        private float _triggerValue;

        public float TriggerValue
        {
            get { return _triggerValue; }
            set
            {
                _triggerValue = value;
                TriggerValueChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<float> TriggerValueChanged; 

        public MorphControllerTriggerButton(string buttonName) : base(buttonName)
        {
        }
    }
}
