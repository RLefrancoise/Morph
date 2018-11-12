using Morph.Core;
using UnityEngine;

namespace Morph.Examples
{
    public class MorphExampleTestCube : MonoBehaviour
    {
        private bool _rotate;

        public void OnFocusEnter()
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        public void OnFocusExit()
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        public void OnSelect()
        {
            _rotate = !_rotate;
        }

        public void OnGrabbed()
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }

        public void OnRelease()
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        void Start()
        {
            transform.position = new Vector3(transform.position.x, MorphMain.Instance.Application.MainDisplay.Camera.transform.position.y, transform.position.z);
        }

        void Update()
        {
            if (!_rotate) return;

            transform.Rotate(Vector3.one * 45f * Time.deltaTime);
        }
    }
}