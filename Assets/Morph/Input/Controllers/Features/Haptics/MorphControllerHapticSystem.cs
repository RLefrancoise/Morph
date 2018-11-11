namespace Morph.Input.Controllers.Features.Haptics
{
    /// <summary>
    /// Morph controller haptic system
    /// </summary>
    public abstract class MorphControllerHapticSystem
    {
        /// <summary>
        /// Set vibration of controller. Zero frequency and amplitude will stop the vibration.
        /// </summary>
        /// <param name="frequency">Frequency of vibration (between 0 and 1)</param>
        /// <param name="amplitude">Amplitude of vibration (between 0 and 1)</param>
        public abstract void SetControllerVibration(float frequency, float amplitude);
    }
}
