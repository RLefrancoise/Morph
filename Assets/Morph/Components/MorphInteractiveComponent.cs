using Morph.Other;
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

        protected override void Start()
        {
            base.Start();
            Collider = GetComponent<Collider>();
        }

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
