using Morph.Components.Interaction.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Focus
{
    /// <inheritdoc />
    /// <summary>
    /// Focus with Unity Event trigger
    /// </summary>
    [RequireComponent(typeof(MorphComponentFocus))]
    public class MorphComponentFocusWithEventTrigger : MorphComponentInteractionWithEventTrigger<MorphComponentFocus>
    {
        public MorphComponentFocus Focus { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Focus = GetComponent<MorphComponentFocus>();

            //Focus
            EventTrigger.Entry focusEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };

            focusEntry.callback.AddListener(eventData =>
            {
                Focused();
            });

            Trigger.triggers.Add(focusEntry);

            //Unfocus
            EventTrigger.Entry unfocusEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit,
                callback = new EventTrigger.TriggerEvent()
            };

            unfocusEntry.callback.AddListener(eventData =>
            {
                Unfocused();
            });

            Trigger.triggers.Add(unfocusEntry);
        }

        private void Focused()
        {
            Focus.Focus();
        }

        private void Unfocused()
        {
            Focus.Unfocus();
        }
    }
}
