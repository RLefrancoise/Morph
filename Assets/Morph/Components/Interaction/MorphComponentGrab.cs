using System;
using Morph.Other;
using UnityEngine;
using UnityEngine.Events;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponentInteraction{TMorphComponentInteraction}"/>
    /// <inheritdoc cref="IMorphComponentGrab"/>
    /// <summary>
    /// Morph component grab
    /// </summary>
    public class MorphComponentGrab : MorphComponentInteraction<MorphComponentGrab>, IMorphComponentGrab
    {
        [SerializeField]
        private UnityEvent _grabbed;
        [SerializeField]
        private UnityEvent _released;

        public bool IsGrabbed { get; protected set; }

        public void Grab()
        {
            IsGrabbed = true;
            Grabbed?.Invoke(this, EventArgs.Empty);
        }

        public void Release()
        {
            IsGrabbed = false;
            Released?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Grabbed;
        public event EventHandler Released;

        public override void Accept(IMorphComponentInteractionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override void Start()
        {
            base.Start();

            Grabbed += (e, args) =>
            {
                _grabbed?.Invoke();
            };

            Released += (e, args) =>
            {
                _released?.Invoke();
            };
        }
    }
}
