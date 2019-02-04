using System;
using Morph.Input.Controllers.Features.Gestures;

namespace Morph.Input.Controllers.Features
{
    /// <inheritdoc />
    /// <summary>
    /// Morph controller gestures feature
    /// </summary>
    public class MorphFeatureGestures : IMorphControllerFeature
    {
        /// <summary>
        /// Recognizable gestures
        /// </summary>
        public IMorphControllerGesture[] Gestures { get; }

        /// <summary>
        /// Gesture recognized event
        /// </summary>
        public event EventHandler<IMorphControllerGesture> GestureRecognized;

        public MorphFeatureGestures(IMorphControllerGesture[] gestures)
        {
            Gestures = gestures;
        }
    }
}
