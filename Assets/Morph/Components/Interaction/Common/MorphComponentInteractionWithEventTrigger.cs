using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Common
{
    /// <inheritdoc cref="MorphComponentInteractionExtension{TMorphComponentInteraction}"/>
    /// <summary>
    /// Morph component interaction using Unity EventTrigger
    /// </summary>
    [RequireComponent(typeof(EventTrigger))]
    public class MorphComponentInteractionWithEventTrigger<TMorphComponentInteraction> : MorphComponentInteractionExtension<TMorphComponentInteraction> where TMorphComponentInteraction : IMorphComponentInteraction
    {
        /// <summary>
        /// Event trigger
        /// </summary>
        public EventTrigger Trigger { get; private set; }

        /// <summary>
        /// Event trigger entries
        /// </summary>
        private List<EventTrigger.Entry> Entries { get; set; }

        protected virtual void OnEnable()
        {
            foreach (var entry in Entries)
            {
                Trigger.triggers.Add(entry);
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var entry in Entries)
            {
                Trigger.triggers.Remove(entry);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Trigger = GetComponent<EventTrigger>();
            if (!Trigger) Trigger = gameObject.AddComponent<EventTrigger>();

            Entries = new List<EventTrigger.Entry>();
        }

        protected void AddEntry(EventTrigger.Entry entry)
        {
            Entries.Add(entry);
        }

        protected void RemoveEntry(EventTrigger.Entry entry)
        {
            Entries.Remove(entry);
        }

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
