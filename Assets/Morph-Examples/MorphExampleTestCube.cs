using UnityEngine;

namespace Morph.Examples
{
    public class MorphExampleTestCube : MonoBehaviour
    {
        public void OnFocusEnter()
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        public void OnFocusExit()
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}