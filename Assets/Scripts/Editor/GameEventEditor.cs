using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Raise"))
            {
                var s = targets.Cast<GameEvent>();
                foreach (var item in s)
                {
                    item.Raise();
                }
            }
        }
    }  
}
