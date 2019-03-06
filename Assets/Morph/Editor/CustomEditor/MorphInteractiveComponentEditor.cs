using Morph.Components;
using UnityEditor;
using UnityEngine;

namespace Morph.Editor.CustomEditor
{
    [UnityEditor.CustomEditor(typeof(MorphInteractiveComponent))]
    public class MorphInteractiveComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _interactable;
        private MorphInteractiveComponent _interactiveComponent;
        
        private void OnEnable()
        {
            _interactable = serializedObject.FindProperty("interactable");
            _interactiveComponent = (MorphInteractiveComponent) target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_interactable);
            
            if (Application.isPlaying)
            {
                _interactiveComponent.Interactable = _interactable.boolValue;
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}