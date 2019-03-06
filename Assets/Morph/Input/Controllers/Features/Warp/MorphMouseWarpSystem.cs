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
    [RequireComponent(typeof(LineRenderer))]
    public class MorphMouseWarpSystem : MorphComponent, IMorphWarpSystem
    {
        private GameObject _warpMarkInstance;
        private Renderer[] _warpMarkInstanceRenderers;
        private RaycastHit _raycastHit;
        private Collider _hitCollider;
        private IMorphWarpZone _zoneBeingHit;
        private bool _isHittingSomething;

        private LineRenderer _lineRenderer;
        private Vector3[] _linePositions;
        private GradientAlphaKey[] _lineGradientAlphaKeys;
        private GradientColorKey[] _lineGradientColorKeys;
        
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
            set
            {
                guidance = value;
                switch (value)
                {
                    case MorphWarpSystemGuidance.Straight:
                        _linePositions = new Vector3[2];
                        _lineGradientAlphaKeys = new[]
                        {
                            new GradientAlphaKey(0f, 0f),
                            new GradientAlphaKey(1f, 0.5f),
                        };
                        _lineGradientColorKeys = new[]
                        {
                            new GradientColorKey(Color.black, 0f),
                            new GradientColorKey(Color.black, 1f)
                        };
                        break;
                    case MorphWarpSystemGuidance.Bezier:
                        break;
                }
            }
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

            _lineRenderer = GetComponent<LineRenderer>();

            //To create line positions and stuff
            Guidance = guidance;
        }
        
        private void Update()
        {
            //Only warp if mouse right click is pressed
            if (!UnityEngine.Input.GetMouseButton(1))
            {
                _warpMarkInstance.gameObject.SetActive(false);
                _lineRenderer.enabled = false;
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
            
            //Show line only if hitting something
            _lineRenderer.enabled = _isHittingSomething;
            
            if (_isHittingSomething)
            {
                _warpMarkInstance.transform.position = _raycastHit.point + _raycastHit.normal * 0.001f;
                _warpMarkInstance.transform.rotation = Quaternion.LookRotation(_raycastHit.normal);

                var color = _zoneBeingHit != null ? availableZoneColor : notAvailableZoneColor;
                
                //Is hitting a warp zone, let's apply a color to notify that user can warp there or not
                foreach (var r in _warpMarkInstanceRenderers)
                {
                    r.material.SetColor(EmissionColor, color);
                }

                //Line renderer color
                for (var i = 0; i < _lineGradientColorKeys.Length; ++i)
                {
                    _lineGradientColorKeys[i].color = color;
                }
                
                var gradient = new Gradient();
                gradient.SetKeys(_lineGradientColorKeys, _lineGradientAlphaKeys);
                _lineRenderer.colorGradient = gradient;
                
                //Line positions
                _lineRenderer.SetPositions(_linePositions);
                
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
            var mouseScreenPoint = new Vector3(
                UnityEngine.Input.mousePosition.x,
                UnityEngine.Input.mousePosition.y,
                MorphMain.Instance.Application.PlayerController.PlayerCamera.nearClipPlane);
            var mouseRay = MorphMain.Instance.Application.PlayerController.PlayerCamera.ScreenPointToRay(
                mouseScreenPoint);
            
            _isHittingSomething = Physics.Raycast(mouseRay, out _raycastHit);
            
            //If it is hitting something
            if(_isHittingSomething)
            {
                //if hit collider is different than previous one
                if (_raycastHit.collider != _hitCollider)
                {
                    _hitCollider = _raycastHit.collider;
                    _zoneBeingHit = _raycastHit.transform.GetComponent<IMorphWarpZone>();
                }
                
                _linePositions[0] =
                    MorphMain.Instance.Application.PlayerController.PlayerCamera.ScreenToWorldPoint(mouseRay.origin);
                _linePositions[1] = _raycastHit.point;
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