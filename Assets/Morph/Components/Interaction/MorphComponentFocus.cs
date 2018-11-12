using System;
using UnityEngine;
using UnityEngine.Events;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponentInteraction{TMorphComponentInteraction}"/>
    /// <inheritdoc cref="IMorphComponentFocus"/>
    /// <summary>
    /// Morph component focus
    /// </summary>
    public class MorphComponentFocus : MorphComponentInteraction<MorphComponentFocus>, IMorphComponentFocus
    {
        [SerializeField]
        private UnityEvent _enteredFocus;
        [SerializeField]
        private UnityEvent _exitedFocus;

        public bool IsFocused { get; protected set; }

        public void Focus()
        {
            if (IsFocused) return;

            IsFocused = true;
            EnteredFocus?.Invoke(this, EventArgs.Empty);
        }

        public void Unfocus()
        {
            if (!IsFocused) return;

            IsFocused = false;
            ExitedFocus?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler EnteredFocus;
        public event EventHandler ExitedFocus;

        public override void Accept(IMorphComponentInteractionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region MonoBehaviour

        protected override void Start()
        {
            base.Start();

            EnteredFocus += (e, args) =>
            {
                _enteredFocus?.Invoke();
            };

            ExitedFocus += (e, args) =>
            {
                _exitedFocus?.Invoke();
            };
        }

        #endregion
    }
}
