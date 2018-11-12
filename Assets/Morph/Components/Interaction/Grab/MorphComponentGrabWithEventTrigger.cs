using System;
using Morph.Components.Interaction.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Grab
{
    /// <inheritdoc />
    /// <summary>
    /// Grab with Unity Event trigger
    /// </summary>
    [RequireComponent(typeof(MorphComponentGrab))]
    public class MorphComponentGrabWithEventTrigger : MorphComponentInteractionWithEventTrigger<MorphComponentGrab>
    {
        public MorphComponentGrab Grab { get; private set; }

        /// <summary>
        /// Grab will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> GrabValidation { get; set; }

        /// <summary>
        /// Release will occur only if predicate returns true
        /// </summary>
        public Predicate<BaseEventData> ReleaseValidation { get; set; }

        /// <summary>
        /// Called while grabbed
        /// </summary>
        public Action<BaseEventData> WhileGrabbed { get; set; }

        /// <summary>
        /// Event trigger entry for grab
        /// </summary>
        protected virtual EventTrigger.Entry GrabEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.BeginDrag,
            callback = new EventTrigger.TriggerEvent()
        };

        /// <summary>
        /// Event trigger entry for release
        /// </summary>
        protected virtual EventTrigger.Entry ReleaseEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.EndDrag,
            callback = new EventTrigger.TriggerEvent()
        };

        /// <summary>
        /// Event trigger entry while grabbed
        /// </summary>
        protected virtual EventTrigger.Entry WhileGrabbedEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.Drag,
            callback = new EventTrigger.TriggerEvent()
        };

        /// <summary>
        /// Use WhileGrabbedEntry ?
        /// </summary>
        protected virtual bool UseGrabbedEntry => true;

        protected override void Start()
        {
            base.Start();

            Grab = GetComponent<MorphComponentGrab>();

            //Grab
            Trigger.triggers.Add(GrabEntry);

            GrabEntry.callback.AddListener(eventData =>
            {
                if (GrabValidation == null || GrabValidation(eventData)) Grabbed();
            });

            //Release
            Trigger.triggers.Add(ReleaseEntry);

            ReleaseEntry.callback.AddListener(eventData =>
            {
                if (ReleaseValidation == null || ReleaseValidation(eventData)) Released();
            });

            //While grabbed
            if (UseGrabbedEntry)
            {
                Trigger.triggers.Add(WhileGrabbedEntry);
                WhileGrabbedEntry.callback.AddListener(WhileGrabbedCallback);
            }
        }

        protected virtual void Grabbed()
        {
            Grab.Grab();
        }

        protected virtual void Released()
        {
            Grab.Release();
        }

        protected virtual void WhileGrabbedCallback(BaseEventData eventData)
        {
            WhileGrabbed?.Invoke(eventData);
        }
    }
}
