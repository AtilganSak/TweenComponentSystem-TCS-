using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DORotate))]
    public class DORotateEditor : Editor
    {
        private DORotate script { get => target as DORotate; }

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
            script.startValue = EditorGUILayout.Vector3Field("Start Value", script.startValue);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyStartValue },
                new DOBase.ContextItem { name = "Record", function = RecordStart });

            script.endValue = EditorGUILayout.Vector3Field("End Value", script.endValue);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyEndValue },
                new DOBase.ContextItem { name = "Record", function = RecordEnd });
            EditorGUILayout.BeginHorizontal();
            script.rotateMode = (DG.Tweening.RotateMode)EditorGUILayout.EnumPopup("Rotate Mode", script.rotateMode);
            script.local = EditorGUILayout.Toggle("Local", script.local);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            Undo.RecordObject(this, "ChangedStartValue");
            if (script.local)
            {
                script.startValue = script.transform.localEulerAngles;
            }
            else
            {
                script.startValue = script.transform.eulerAngles;
            }
            EditorUtility.SetDirty(this);
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            if (script.local)
            {
                script.transform.localRotation = Quaternion.Euler(script.startValue);
            }
            else
            {
                script.transform.rotation = Quaternion.Euler(script.startValue);
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            Undo.RecordObject(this, "ChangedEndValue");
            if (script.local)
            {
                script.endValue = script.transform.localEulerAngles;
            }
            else
            {
                script.endValue = script.transform.eulerAngles;
            }
            EditorUtility.SetDirty(script);

        }
        void ApplyEndValue()
        {
            Undo.RecordObject(script.transform, "ApplyedEndValue");
            if (script.local)
            {
                script.transform.localRotation = Quaternion.Euler(script.endValue);
            }
            else
            {
                script.transform.rotation = Quaternion.Euler(script.endValue);
            }
            EditorUtility.SetDirty(script);

        }
    }
}
