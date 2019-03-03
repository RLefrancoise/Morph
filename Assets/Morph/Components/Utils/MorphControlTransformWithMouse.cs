using UnityEngine;

namespace Morph.Components.Utils
{
    /// <inheritdoc />
    /// <summary>
    /// Control a transform with mouse
    /// </summary>
    public class MorphControlTransformWithMouse : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToControl;

        private Vector3? _previousMousePosition;

        public Transform TransformToControl
        {
            get
            {
                return transformToControl;
            }
            set
            {
                transformToControl = value;
            } 
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                _previousMousePosition = UnityEngine.Input.mousePosition;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(1))
            {
                _previousMousePosition = null;
            }

            if (_previousMousePosition == null) return;
            
            var mouseDelta = UnityEngine.Input.mousePosition - _previousMousePosition.Value;
            transformToControl.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * Screen.width / 180f);
            transformToControl.Rotate(Vector3.right, mouseDelta.y * Time.deltaTime * Screen.height / 180f);
        }
    }
}