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
        public MorphControllerGesture[] Gestures { get; set; }

        /// <summary>
        /// Gesture recognized event
        /// </summary>
        public event EventHandler<MorphControllerGesture> GestureRecognized;
    }
}
