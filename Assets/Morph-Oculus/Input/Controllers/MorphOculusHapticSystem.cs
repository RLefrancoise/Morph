using Morph.Input.Controllers.Features.Haptics;

namespace Morph.Input.Controllers.Oculus
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller haptic system for Oculus
    /// </summary>
    public class MorphOculusHapticSystem : IMorphControllerHapticSystem
    {
        /// <summary>
        /// Controller of the haptic system
        /// </summary>
        public OVRInput.Controller Controller { get; }

        /// <summary>
        /// Create a new Morph Oculus haptic system
        /// </summary>
        /// <param name="controller">controller associated with this haptic system</param>
        public MorphOculusHapticSystem(OVRInput.Controller controller)
        {
            Controller = controller;
        }

        public void SetControllerVibration(float frequency, float amplitude)
        {
            if (!OVRInput.IsControllerConnected(Controller)) return;
            OVRInput.SetControllerVibration(frequency, amplitude, Controller);
        }
    }
}
