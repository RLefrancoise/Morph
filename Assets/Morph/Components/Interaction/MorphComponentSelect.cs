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

        public bool IsSelected { get; protected set; }

        public void Select()
        {
            if (!IsSelected) return;

            IsSelected = true;
            Selected?.Invoke(this, EventArgs.Empty);
        }

        public void Deselect()
        {
            if (!IsSelected) return;

            IsSelected = false;
            Deselected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Selected;
        public event EventHandler Deselected;

        public override void Accept(IMorphComponentInteractionVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override void Start()
        {
            base.Start();

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
