using Morph.Input.Controllers.Features.Buttons;

namespace Morph.Input.Controllers.Oculus.Features.Buttons
{
    /// <inheritdoc />
    /// <summary>
    /// Morph Oculus tracked remote controller button
    /// </summary>
    public class MorphOculusTrackedRemoteControllerButton : MorphControllerButton
    {
        public MorphOculusTrackedRemoteControllerButton(string buttonName) : base(buttonName)
        {
        }

        internal void SetPressed(bool pressed)
        {
            Pressed = pressed;
        }
    }
}
