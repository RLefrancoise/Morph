using Morph.Components.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Morph.Components
{
    /// <inheritdoc cref="IMorphInteractiveComponent"/>
    /// <summary>
    /// Morph interactive component
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class MorphInteractiveComponent : MorphComponent, IMorphInteractiveComponent
    {
        [SerializeField]
        private bool interactable = true;

        public bool Interactable
        {
            get { return interactable; }
            set
            {
                interactable = value;
                Collider.enabled = value;

                var interactions = GetComponents<IMorphComponentInteraction>();
                foreach (var interaction in interactions)
                {
                    (interaction as MonoBehaviour).enabled = value;
                }
            }
        }

        public Collider Collider { get; protected set; }

        protected override void Start()
        {
            base.Start();
            Collider = GetComponent<Collider>();
            Interactable = interactable;
        }

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
