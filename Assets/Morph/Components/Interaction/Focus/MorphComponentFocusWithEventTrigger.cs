﻿using System;
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

        /// <summary>
        /// Focus will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> FocusValidation { get; set; }

        /// <summary>
        /// Unfocus will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> UnfocusValidation { get; set; }

        protected override void Start()
        {
            base.Start();

            Focus = GetComponent<MorphComponentFocus>();

            //Focus
            EventTrigger.Entry focusEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };

            focusEntry.callback.AddListener(eventData =>
            {
                if (FocusValidation == null || FocusValidation(eventData)) Focused();
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
                if (UnfocusValidation == null || UnfocusValidation(eventData)) Unfocused();
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
