using Morph.Core;
using UnityEngine;

namespace Morph.Components
{
    /// <inheritdoc cref="IMorphComponent"/>
    /// <summary>
    /// Abstract Morph component. You can derive this class to implement a new Morph component.
    /// </summary>
    public abstract class MorphComponent : MonoBehaviour, IMorphComponent
    {
        protected virtual void Start()
        {
            MorphMain.Instance.Application.RegisterComponent(this);
        }

        protected virtual void OnDestroy()
        {
            MorphMain.Instance?.Application?.UnregisterComponent(this);
        }

        public abstract void Accept(IMorphComponentVisitor visitor);
    }
}
