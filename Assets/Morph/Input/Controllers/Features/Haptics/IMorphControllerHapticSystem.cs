namespace Morph.Input.Controllers.Features.Haptics
{
    /// <summary>
    /// Morph controller haptic system
    /// </summary>
    public interface IMorphControllerHapticSystem
    {
        /// <summary>
        /// Set vibration of controller. Zero frequency and amplitude will stop the vibration.
        /// </summary>
        /// <param name="frequency">Frequency of vibration (between 0 and 1)</param>
        /// <param name="amplitude">Amplitude of vibration (between 0 and 1)</param>
        void SetControllerVibration(float frequency, float amplitude);
    }
}
