using UnityEngine;

namespace Morph.Examples
{
    public class MorphExampleTestCube : MonoBehaviour
    {
        [SerializeField]
        private Color initialColor = Color.cyan;
        [SerializeField]
        private Color focusColor = Color.green;
        [SerializeField]
        private Color grabColor = Color.yellow;
        
        private bool _rotate;
        
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        public void OnFocusEnter()
        {
            GetComponent<Renderer>().material.SetColor(EmissionColor, focusColor);
        }

        public void OnFocusExit()
        {
            GetComponent<Renderer>().material.SetColor(EmissionColor, initialColor);
        }

        public void OnSelect()
        {
            _rotate = !_rotate;
        }

        public void OnGrabbed()
        {
            GetComponent<Renderer>().material.SetColor(EmissionColor, grabColor);
        }

        public void OnRelease()
        {
            GetComponent<Renderer>().material.SetColor(EmissionColor, initialColor);
        }

        private void Start()
        {
            OnFocusExit();
        }

        private void Update()
        {
            if (!_rotate) return;

            transform.Rotate(Vector3.one * 45f * Time.deltaTime);
        }
    }
}