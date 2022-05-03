using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOAnchorPos))]
    public class DOAnchorEditor : Editor
    {
        private DOAnchorPos script { get => target as DOAnchorPos; }

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
                script.startTarget = (RectTransform)EditorGUILayout.ObjectField("Start Target", script.startTarget, typeof(RectTransform), true);
            else
            {
                script.startValue = EditorGUILayout.Vector3Field("Start Value", script.startValue);
                script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyStartValue },
                    new DOBase.ContextItem { name = "Record", function = RecordStart });
            }

            script.useEndTarget = EditorGUILayout.Toggle("Use End Target", script.useEndTarget);
            if (script.useEndTarget)
                script.endTarget = (RectTransform)EditorGUILayout.ObjectField("End Target", script.endTarget, typeof(RectTransform), true);
            else
            {
                script.endValue = EditorGUILayout.Vector3Field("End Value", script.endValue);
                script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyEndValue },
                    new DOBase.ContextItem { name = "Record", function = RecordEnd });
            }
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            if (!script.useStartTarget)
            {
                Undo.RecordObject(this, "ChangedStartValue");
                script.startValue = script.GetComponent<RectTransform>().anchoredPosition;
                EditorUtility.SetDirty(this);
            }
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            if (!script.useStartTarget)
            {
                script.GetComponent<RectTransform>().anchoredPosition = script.startValue;
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            if (!script.useEndTarget)
            {
                Undo.RecordObject(this, "ChangedEndValue");
                script.endValue = script.GetComponent<RectTransform>().anchoredPosition;
                EditorUtility.SetDirty(script);
            }
        }
        void ApplyEndValue()
        {
            if (!script.useEndTarget)
            {
                Undo.RecordObject(script.transform, "ApplyedEndValue");
                script.GetComponent<RectTransform>().anchoredPosition = script.endValue;
                EditorUtility.SetDirty(script);
            }
        }
    }
}
