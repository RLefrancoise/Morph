using System;
using Morph.Other;
using UnityEngine;
using UnityEngine.Events;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponentInteraction{TMorphComponentInteraction}"/>
    /// <inheritdoc cref="IMorphComponentSelect"/>
    /// <summary>
    /// Morph component select
    /// </summary>
    public class MorphComponentSelect : MorphComponentInteraction<MorphComponentSelect>, IMorphComponentSelect
    {
        [SerializeField]
        private UnityEvent _selected;
        [SerializeField]
        private UnityEvent _deselected;

        public void Select()
        {
            Selected?.Invoke(this, EventArgs.Empty);
        }

        public void Deselect()
        {
            Deselected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Selected;
        public event EventHandler Deselected;

        public override void Accept(IMorphComponentInteractionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override void Awake()
        {
            base.Awake();

            Selected += (e, args) =>
            {
                _selected?.Invoke();
            };

            Deselected += (e, args) =>
            {
                _deselected?.Invoke();
            };
        }
    }
}
