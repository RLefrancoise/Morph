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
    [RequireComponent(typeof(MorphMouseController))]
    public class MorphMouseWarpSystem : MorphComponent, IMorphWarpSystem
    {
        private MorphMouseController _mouseController;
        
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
        [SerializeField]
        public float maxWarpDistance = 2f;
        [SerializeField]
        public int curveSegmentsNumber = 32;
        
        private static readonly int EmissionColor = Shader.PropertyToID("_Color");

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
                    case MorphWarpSystemGuidance.Curve:
                        _linePositions = new Vector3[CurveGuidanceSegments];
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
                }
            }
        }

        /// <inheritdoc />
        public float MaxWarpDistance
        {
            get { return maxWarpDistance; }
            set { maxWarpDistance = value; }
        }

        /// <inheritdoc />
        public int CurveGuidanceSegments 
        {
            get { return curveSegmentsNumber; }
            set
            {
                curveSegmentsNumber = value;
                _linePositions = new Vector3[curveSegmentsNumber];
            }
        }

        /// <inheritdoc />
        public bool IsGuidanceSupported(MorphWarpSystemGuidance warpSystemGuidance)
        {
            switch (warpSystemGuidance)
            {
                case MorphWarpSystemGuidance.Straight:
                case MorphWarpSystemGuidance.Curve:
                    return true;
                default:
                    return false;
            }
        }

        protected override void Start()
        {
            base.Start();

            _mouseController = GetComponent<MorphMouseController>();
            
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
                _isHittingSomething = false;
                _zoneBeingHit = null;
                _hitCollider = null;
                return;
            }
            
            switch (Guidance)
            {
                case MorphWarpSystemGuidance.Straight:
                    UpdateStraight();
                    break;
                case MorphWarpSystemGuidance.Curve:
                    UpdateCurve();
                    break;
            }

            //update mark
            _warpMarkInstance.gameObject.SetActive(_isHittingSomething);
            
            //Show line only if hitting something
            _lineRenderer.enabled = true;
            
            //Line positions
            _lineRenderer.positionCount = _linePositions.Length;
            _lineRenderer.SetPositions(_linePositions);
            
            if (_isHittingSomething)
            {
                //if hit collider is different than previous one
                if (_raycastHit.collider != _hitCollider)
                {
                    _hitCollider = _raycastHit.collider;
                    _zoneBeingHit = _raycastHit.transform.GetComponent<IMorphWarpZone>();
                }
                
                _warpMarkInstance.transform.position = _raycastHit.point + _raycastHit.normal * 0.002f;
                _warpMarkInstance.transform.rotation = Quaternion.LookRotation(_raycastHit.normal);

                var color = _zoneBeingHit != null ? availableZoneColor : notAvailableZoneColor;
                
                //check distance to see if destination is too far
                var playerPosition = MorphMain.Instance.Application.PlayerController.PlayerTransform.position;
                var hitDistance = Vector3.Distance(
                    playerPosition,
                    new Vector3(_raycastHit.point.x, playerPosition.y, _raycastHit.point.z));
                var zoneTooFar = hitDistance > MaxWarpDistance;
                
                if (MaxWarpDistance > 0f && zoneTooFar)
                    color = notAvailableZoneColor;
                
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
                
                //If left click, warp player
                if (UnityEngine.Input.GetMouseButtonDown(0) && _zoneBeingHit != null && !zoneTooFar)
                {
                    var warpPosition = MorphMain.Instance.Application.PlayerController.PlayerTransform.position;
                    warpPosition = new Vector3(_raycastHit.point.x, warpPosition.y, _raycastHit.point.z);
                    MorphMain.Instance.Application.PlayerController.PlayerTransform.position = warpPosition;
                }
            }
            else
            {
                _hitCollider = null;
                _zoneBeingHit = null;
                
                //Line renderer color
                for (var i = 0; i < _lineGradientColorKeys.Length; ++i)
                {
                    _lineGradientColorKeys[i].color = notAvailableZoneColor;
                }
                
                var gradient = new Gradient();
                gradient.SetKeys(_lineGradientColorKeys, _lineGradientAlphaKeys);
                _lineRenderer.colorGradient = gradient;
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
                _linePositions[0] = MorphMain.Instance.Application.PlayerController.PlayerCamera.transform.position;
                _linePositions[1] = _raycastHit.point;
            }
        }

        private void UpdateCurve()
        {
            _isHittingSomething = false;
            
            var playerCamera = MorphMain.Instance.Application.PlayerController.PlayerCamera;
            var mouseScreenPoint = new Vector3(
                UnityEngine.Input.mousePosition.x,
                UnityEngine.Input.mousePosition.y,
                playerCamera.nearClipPlane);
            
            var mouseRay = playerCamera.ScreenPointToRay(mouseScreenPoint);
            var speed = MaxWarpDistance / 2f;
            var velocity = mouseRay.direction * speed;
            var gravity = Vector3.down;

            for (var i = 0; i < CurveGuidanceSegments; ++i)
            {
                var time = (float) i / CurveGuidanceSegments * MaxWarpDistance;
                var arcPos = _mouseController.Position.Position + ((velocity * time) + (0.5f * time * time) * gravity);
                _linePositions[i] = arcPos;

                if (i > 0)
                {
                    _isHittingSomething = Physics.Linecast(_linePositions[i - 1], _linePositions[i], out _raycastHit);
                }

                //If hitting something, stop curve at colliding point
                if (_isHittingSomething)
                {
                    _linePositions[i] = _raycastHit.point;
                    
                    for (var j = i + 1; j < CurveGuidanceSegments; ++j)
                        _linePositions[j] = _linePositions[i];
                    
                    break;
                }
            }
        }

        /// <inheritdoc />
        public override void Accept(IMorphComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}