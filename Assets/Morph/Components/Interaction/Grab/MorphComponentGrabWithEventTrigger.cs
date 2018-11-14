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

        protected override void Awake()
        {
            base.Awake();

            Grab = GetComponent<MorphComponentGrab>();

            //Grab
            EventTrigger.Entry grabEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.BeginDrag,
                callback = new EventTrigger.TriggerEvent()
            };

            grabEntry.callback.AddListener(eventData =>
            {
                if (GrabValidation == null || GrabValidation(eventData))
                {
                    Debug.Log("grabbed");
                    Grabbed();
                }
            });

            //Release
            EventTrigger.Entry releaseEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.EndDrag,
                callback = new EventTrigger.TriggerEvent()
            };

            releaseEntry.callback.AddListener(eventData =>
            {
                Debug.Log("released");
                if (ReleaseValidation == null || ReleaseValidation(eventData)) Released();
            });

            //While grabbed
            EventTrigger.Entry whileGrabbedEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag,
                callback = new EventTrigger.TriggerEvent()
            };

            whileGrabbedEntry.callback.AddListener(WhileGrabbedCallback);

            AddEntry(grabEntry);
            AddEntry(releaseEntry);
            AddEntry(whileGrabbedEntry);
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
