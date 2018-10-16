using UnityEngine.EventSystems;

namespace Morph.Components.Interaction.Grab.GoogleVR
{
    /// <inheritdoc />
    /// <summary>
    /// Morph component grab for Google VR reticle
    /// </summary>
    public class MorphComponentGoogleVrReticleGrab : MorphComponentGrabWithEventTrigger
    {
        protected override EventTrigger.Entry GrabEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown,
            callback = new EventTrigger.TriggerEvent()
        };

        protected override EventTrigger.Entry ReleaseEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp,
            callback = new EventTrigger.TriggerEvent()
        };

        protected override EventTrigger.Entry WhileGrabbedEntry => new EventTrigger.Entry
        {
            eventID = EventTriggerType.UpdateSelected,
            callback = new EventTrigger.TriggerEvent()
        };

        

        protected override void WhileGrabbedCallback(BaseEventData eventData)
        {

            base.WhileGrabbedCallback(eventData);
        }
    }
}
