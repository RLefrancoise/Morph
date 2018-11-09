using Morph.Components;
using UnityEngine;

namespace Morph.Core.Displays
{
    /// <inheritdoc cref="MorphComponent"/>
    /// <inheritdoc cref="IMorphDisplay"/>
    /// <summary>
    /// Default Morph display
    /// </summary>
    public class MorphDefaultDisplay : MorphComponent, IMorphDisplay
    {
        [SerializeField]
        private MorphDisplayType _displayType;

        public MorphDisplayType DisplayType { get; private set; }
        public Camera Camera { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DisplayType = _displayType;
            Camera = GetComponent<Camera>();
        }

        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
