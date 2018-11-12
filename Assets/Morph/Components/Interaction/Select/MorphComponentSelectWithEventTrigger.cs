using System;
using Morph.Components.Interaction.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Select
{
    /// <inheritdoc />
    /// <summary>
    /// Select with Unity Event trigger
    /// </summary>
    [RequireComponent(typeof(MorphComponentSelect))]
    public class MorphComponentSelectWithEventTrigger : MorphComponentInteractionWithEventTrigger<MorphComponentSelect>
    {
        public MorphComponentSelect Select { get; private set; }

        /// <summary>
        /// Select will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> SelectValidation { get; set; }

        /// <summary>
        /// Deselect will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> DeselectValidation { get; set; }

        protected override void Start()
        {
            base.Start();

            Select = GetComponent<MorphComponentSelect>();

            //Select
            EventTrigger.Entry selectEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown,
                callback = new EventTrigger.TriggerEvent()
            };

            selectEntry.callback.AddListener(eventData =>
            {
                if(SelectValidation == null || SelectValidation(eventData)) Selected();
            });

            Trigger.triggers.Add(selectEntry);

            //Deselect
            EventTrigger.Entry deselectEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp,
                callback = new EventTrigger.TriggerEvent()
            };

            deselectEntry.callback.AddListener(eventData =>
            {
                if (DeselectValidation == null || DeselectValidation(eventData)) Deselected();
            });

            Trigger.triggers.Add(deselectEntry);
        }

        private void Selected()
        {
            Select.Select();
        }

        private void Deselected()
        {
            Select.Deselect();
        }
    }
}
