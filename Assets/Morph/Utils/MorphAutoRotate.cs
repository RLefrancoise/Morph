using UnityEngine;

namespace Morph.Utils
{
    /// <summary>
    /// Auto rotate
    /// </summary>
    public class MorphAutoRotate : MonoBehaviour
    {
        public Vector3 rotationSpeed;

        private void Update()
        {
            transform.localRotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime);
        }
    }
}