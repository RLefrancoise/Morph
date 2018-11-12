﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Common
{
    /// <inheritdoc cref="MorphComponentInteractionExtension"/>
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

        protected virtual void OnEnable()
        {
            Trigger.enabled = true;
        }

        protected virtual void OnDisable()
        {
            Trigger.enabled = false;
        }

        protected override void Start()
        {
            base.Start();

            Trigger = GetComponent<EventTrigger>();
            if (!Trigger) Trigger = gameObject.AddComponent<EventTrigger>();
        }

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
