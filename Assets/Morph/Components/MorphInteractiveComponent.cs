using UnityEngine;

namespace Morph.Components
{
    /// <inheritdoc cref="IMorphInteractiveComponent"/>
    /// <summary>
    /// Morph interactive component
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class MorphInteractiveComponent : MorphComponent, IMorphInteractiveComponent
    {
        public Collider Collider { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Collider = GetComponent<Collider>();
        }
    }
}
