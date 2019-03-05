using UnityEngine;

namespace Morph.Utils
{
    /// <inheritdoc />
    /// <summary>
    /// Control a transform with mouse
    /// </summary>
    public class MorphControlTransformWithMouse : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToControl;

        private float _mouseX;
        private float _mouseY;
        
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
            if (!UnityEngine.Input.GetMouseButton(2)) return;
            
            _mouseX += UnityEngine.Input.GetAxis("Mouse X") * 5;
            if (_mouseX <= -180)
            {
                _mouseX += 360;
            } else if (_mouseX > 180)
            {
                _mouseX -= 360;
            }
            _mouseY -= UnityEngine.Input.GetAxis("Mouse Y") * 2.4f;
            _mouseY = Mathf.Clamp(_mouseY, -85, 85);
            
            transformToControl.localRotation = Quaternion.Euler(_mouseY, _mouseX, 0f);
        }
    }
}