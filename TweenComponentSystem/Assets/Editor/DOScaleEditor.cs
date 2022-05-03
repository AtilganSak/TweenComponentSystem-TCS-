using UnityEditor;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOScale))]
    public class DOScaleEditor : Editor
    {
        private DOScale script { get => target as DOScale; }

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
            script.lossy = EditorGUILayout.Toggle("Lossy", script.lossy);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            Undo.RecordObject(this, "ChangedStartValue");
            if (script.lossy)
            {
                script.startValue = script.transform.lossyScale;
            }
            else
            {
                script.startValue = script.transform.localScale;
            }
            EditorUtility.SetDirty(this);
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            if (!script.lossy)
            {
                script.transform.localScale = script.startValue;
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {

            Undo.RecordObject(this, "ChangedEndValue");
            if (script.lossy)
            {
                script.endValue = script.transform.lossyScale;
            }
            else
            {
                script.endValue = script.transform.localScale;
            }
            EditorUtility.SetDirty(script);

        }
        void ApplyEndValue()
        {
            Undo.RecordObject(script.transform, "ApplyedEndValue");
            if (!script.lossy)
            {
                script.transform.localScale = script.endValue;
            }
            EditorUtility.SetDirty(script);

        }
    }
}
