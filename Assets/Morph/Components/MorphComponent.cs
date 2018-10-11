using Morph.Core;
using Morph.Other;
using UnityEngine;

namespace Morph.Components
{
    /// <inheritdoc cref="IMorphComponent"/>
    /// <summary>
    /// Abstract Morph component. You can derive this class to implement a new Morph component.
    /// </summary>
    public abstract class MorphComponent : MonoBehaviour, IMorphComponent
    {
        protected virtual void Awake()
        {
            MorphMain.Instance.Application.RegisterComponent(this);

            /*foreach (var controller in MorphMain.Instance.Application.Controllers)
            {
                Accept(controller);
            }*/
        }

        protected virtual void OnDestroy()
        {
            MorphMain.Instance?.Application?.UnregisterComponent(this);
        }

        /*#region IVisitable

        public virtual void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion*/
    }
}
