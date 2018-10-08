using System;
using UnityEngine;
using UnityEngine.Events;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="IMorphComponentFocus"/>
    /// <summary>
    /// Morph component focus
    /// </summary>
    public class MorphComponentFocus : MorphComponentInteraction, IMorphComponentFocus
    {
        [SerializeField]
        private UnityEvent _enteredFocus;
        [SerializeField]
        private UnityEvent _exitedFocus;

        public void Focus()
        {
            EnteredFocus?.Invoke(this, EventArgs.Empty);
        }

        public void Unfocus()
        {
            ExitedFocus?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler EnteredFocus;
        public event EventHandler ExitedFocus;

        protected override void Awake()
        {
            base.Awake();

            EnteredFocus += (e, args) =>
            {
                _enteredFocus?.Invoke();
            };

            ExitedFocus += (e, args) =>
            {
                _exitedFocus?.Invoke();
            };
        }
    }
}
