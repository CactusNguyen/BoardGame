using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Channel))]
    public class ChannelEditor : UnityEditor.Editor
    {
        private SerializedProperty _listeners;

        private void OnEnable()
        {
            _listeners = serializedObject.FindProperty("_listeners");
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Raise"))
            {
                var s = targets.Cast<Channel>();
                foreach (var item in s)
                    item.Raise();
            }

            if (targets.Length > 1)
            {
                EditorGUILayout.HelpBox("Showing Listener of selected object", MessageType.Info);
            }

            GUI.enabled = false;
            EditorGUILayout.PropertyField(_listeners);
            GUI.enabled = true;
        }
    }
}