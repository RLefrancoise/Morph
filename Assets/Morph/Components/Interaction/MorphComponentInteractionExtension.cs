﻿namespace Morph.Components.Interaction
{
    /// <inheritdoc cref="MorphComponent"/>
    /// <inheritdoc cref="IMorphComponentInteractionExtension{TMorphComponentInteraction}"/>
    /// <summary>
    /// base class for interaction extension
    /// </summary>
    public abstract class MorphComponentInteractionExtension<TMorphComponentInteraction> : MorphComponent, IMorphComponentInteractionExtension<TMorphComponentInteraction> where TMorphComponentInteraction : IMorphComponentInteraction
    {
        public TMorphComponentInteraction Interaction { get; protected set; }

        protected virtual void Awake()
        {
            Interaction = GetComponent<TMorphComponentInteraction>();
        }
    }
}
