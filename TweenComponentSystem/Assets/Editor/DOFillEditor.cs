using UnityEditor;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOFill))]
    public class DOFillEditor : Editor
    {
        private DOFill script { get => target as DOFill; }

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
            script.startValue = EditorGUILayout.FloatField("Start Value", script.startValue);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyStartValue },
                new DOBase.ContextItem { name = "Record", function = RecordStart });

            script.endValue = EditorGUILayout.FloatField("End Value", script.endValue);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyEndValue },
                new DOBase.ContextItem { name = "Record", function = RecordEnd });
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            Undo.RecordObject(this, "ChangedStartValue");
            script.startValue = script.GetComponent<Image>().fillAmount;
            EditorUtility.SetDirty(this);
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            script.GetComponent<Image>().fillAmount = script.startValue;
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            Undo.RecordObject(this, "ChangedEndValue");
            script.endValue = script.GetComponent<Image>().fillAmount;
            EditorUtility.SetDirty(script);

        }
        void ApplyEndValue()
        {
            Undo.RecordObject(script.transform, "ApplyedEndValue");
            script.GetComponent<Image>().fillAmount = script.endValue;
            EditorUtility.SetDirty(script);
        }
    }
}
