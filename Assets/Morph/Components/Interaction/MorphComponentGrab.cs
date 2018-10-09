using System;
using UnityEngine;
using UnityEngine.Events;

namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponentInteraction"/>
    /// <inheritdoc cref="IMorphComponentGrab"/>
    /// <summary>
    /// Morph component grab
    /// </summary>
    public class MorphComponentGrab : MorphComponentInteraction, IMorphComponentGrab
    {
        [SerializeField]
        private UnityEvent _grabbed;
        [SerializeField]
        private UnityEvent _released;

        public void Grab()
        {
            Grabbed?.Invoke(this, EventArgs.Empty);
        }

        public void Release()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Grabbed;
        public event EventHandler Released;

        protected override void Awake()
        {
            base.Awake();

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
