using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Morph component interaction using Unity EventTrigger
    /// </summary>
    /// <typeparam name="TMorphComponentInteraction"></typeparam>
    [RequireComponent(typeof(EventTrigger))]
    public class MorphComponentInteractionWithEventTrigger<TMorphComponentInteraction> : MorphComponent where TMorphComponentInteraction : IMorphComponentInteraction
    {
        /// <summary>
        /// Morph interaction
        /// </summary>
        public TMorphComponentInteraction Interaction { get; private set; }

        /// <summary>
        /// Event trigger
        /// </summary>
        public EventTrigger Trigger { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Trigger = GetComponent<EventTrigger>();
            if (!Trigger) Trigger = gameObject.AddComponent<EventTrigger>();

            Interaction = GetComponent<TMorphComponentInteraction>();
        }
    }
}
