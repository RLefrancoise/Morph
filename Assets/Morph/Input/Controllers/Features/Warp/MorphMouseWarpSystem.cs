using Morph.Components;
using Morph.Components.Navigation;
using Morph.Core;
using UnityEngine;

namespace Morph.Input.Controllers.Features.Warp
{
    /// <inheritdoc cref="MorphComponent"/>
    /// <inheritdoc cref="IMorphWarpSystem"/>
    /// <summary>
    /// Morph warp system for mouse
    /// </summary>
    public class MorphMouseWarpSystem : MorphComponent, IMorphWarpSystem
    {
        private GameObject _warpMarkInstance;
        private Renderer[] _warpMarkInstanceRenderers;
        private RaycastHit _raycastHit;
        private Collider _hitCollider;
        private IMorphWarpZone _zoneBeingHit;
        private bool _isHittingSomething;

        [SerializeField]
        public MorphWarpSystemGuidance guidance;
        [SerializeField]
        public GameObject warpMark;
        [SerializeField]
        public Color availableZoneColor = Color.green;
        [SerializeField]
        public Color notAvailableZoneColor = Color.red;

        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        /// <inheritdoc />
        public MorphWarpSystemGuidance Guidance
        {
            get { return guidance; }
            set { guidance = value; }
        }

        /// <inheritdoc />
        public bool IsGuidanceSupported(MorphWarpSystemGuidance warpSystemGuidance)
        {
            switch (warpSystemGuidance)
            {
                case MorphWarpSystemGuidance.Straight:
                    return true;
                case MorphWarpSystemGuidance.Bezier:
                    return true;
                default:
                    return false;
            }
        }

        protected override void Start()
        {
            base.Start();

            _warpMarkInstance = Instantiate(warpMark, transform);
            _warpMarkInstanceRenderers = _warpMarkInstance.GetComponentsInChildren<Renderer>();
        }
        
        private void Update()
        {
            //Only warp if mouse right click is pressed
            if (!UnityEngine.Input.GetMouseButton(1))
            {
                _warpMarkInstance.gameObject.SetActive(false);
                return;
            }
            
            switch (Guidance)
            {
                case MorphWarpSystemGuidance.Straight:
                    UpdateStraight();
                    break;
                case MorphWarpSystemGuidance.Bezier:
                    UpdateBezier();
                    break;
            }

            //update mark
            _warpMarkInstance.gameObject.SetActive(_isHittingSomething);
            
            if (_isHittingSomething)
            {
                _warpMarkInstance.transform.position = _raycastHit.point + _raycastHit.normal * 0.001f;
                _warpMarkInstance.transform.rotation = Quaternion.LookRotation(_raycastHit.normal);
                
                //Is hitting a warp zone, let's apply a green color to notify that user can warp there
                foreach (var r in _warpMarkInstanceRenderers)
                {
                    r.material.SetColor(EmissionColor, _zoneBeingHit != null ? availableZoneColor : notAvailableZoneColor);
                }

                //If left click, warp player
                if (UnityEngine.Input.GetMouseButtonDown(0) && _zoneBeingHit != null)
                {
                    var warpPosition = MorphMain.Instance.Application.PlayerController.PlayerTransform.position;
                    warpPosition = new Vector3(_raycastHit.point.x, warpPosition.y, _raycastHit.point.z);
                    MorphMain.Instance.Application.PlayerController.PlayerTransform.position = warpPosition;
                }
            }
        }

        private void UpdateStraight()
        {
            var t = transform;
            _isHittingSomething = Physics.Raycast(MorphMain.Instance.Application.PlayerController.PlayerCamera.ScreenPointToRay(
                new Vector3(
                    UnityEngine.Input.mousePosition.x,
                    UnityEngine.Input.mousePosition.y, 
                    MorphMain.Instance.Application.PlayerController.PlayerCamera.nearClipPlane)), out _raycastHit);
            
            //If it is hitting something
            if(_isHittingSomething)
            {
                //if hit collider is different than previous one
                if (_raycastHit.collider != _hitCollider)
                {
                    _hitCollider = _raycastHit.collider;
                    _zoneBeingHit = _raycastHit.transform.GetComponent<IMorphWarpZone>();
                }
            }
            else
            {
                _hitCollider = null;
                _zoneBeingHit = null;
            }
        }

        private void UpdateBezier()
        {
            
        }

        /// <inheritdoc />
        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}