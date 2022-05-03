using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOMove))]
    public class DOMoveEditor : Editor
    {
        private DOMove script { get => target as DOMove; }

        private void OnEnable()
        {
            script.Enable();
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            script.DrawParameters();
            script.DrawToggles();
            DrawValues();
            script.DrawEvents();
            script.DrawTweenKey();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(script);
            }
            script.DrawButtons();
        }
        private void DrawValues()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Values", script.headerStyle);
            script.useStartTarget = EditorGUILayout.Toggle("Use Start Target", script.useStartTarget);
            if (script.useStartTarget)
                script.startTarget = (Transform)EditorGUILayout.ObjectField("Start Target", script.startTarget, typeof(Transform), true);
            else
            {
                script.startValue = EditorGUILayout.Vector3Field("Start Value", script.startValue);
                script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyStartValue },
                    new DOBase.ContextItem { name = "Record", function = RecordStart });
            }

            script.useEndTarget = EditorGUILayout.Toggle("Use End Target", script.useEndTarget);
            if (script.useEndTarget)
                script.endTarget = (Transform)EditorGUILayout.ObjectField("End Target", script.endTarget, typeof(Transform), true);
            else
            {
                script.endValue = EditorGUILayout.Vector3Field("End Value", script.endValue);
                script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyEndValue },
                    new DOBase.ContextItem { name = "Record", function = RecordEnd });
            }
            script.local = EditorGUILayout.Toggle("Local", script.local);
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            if (!script.useStartTarget)
            {
                Undo.RecordObject(this, "ChangedStartValue");
                if (script.local)
                {
                    script.startValue = script.transform.localPosition;
                }
                else
                {
                    script.startValue = script.transform.position;
                }
                EditorUtility.SetDirty(this);
            }
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            if (!script.useStartTarget)
            {
                if (script.local)
                {
                    script.transform.localPosition = script.startValue;
                }
                else
                {
                    script.transform.position = script.startValue;
                }
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            if (!script.useEndTarget)
            {
                Undo.RecordObject(this, "ChangedEndValue");
                if (script.local)
                {
                    script.endValue = script.transform.localPosition;
                }
                else
                {
                    script.endValue = script.transform.position;
                }
                EditorUtility.SetDirty(script);
            }
        }
        void ApplyEndValue()
        {
            if (!script.useEndTarget)
            {
                Undo.RecordObject(script.transform, "ApplyedEndValue");
                if (script.local)
                {
                    script.transform.localPosition = script.endValue;
                }
                else
                {
                    script.transform.position = script.endValue;
                }
                EditorUtility.SetDirty(script);
            }
        }
    }
}
