using Morph.Components.Interaction;
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
        [SerializeField]
        private bool _interactable = true;

        public bool Interactable
        {
            get { return _interactable; }
            set
            {
                _interactable = value;
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
            Interactable = _interactable;
        }

#if UNITY_EDITOR
        protected virtual void Update()
        {
            Interactable = _interactable;
        }
#endif

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
