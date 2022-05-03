using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOFade))]
    public class DOFadeEditor : Editor
    {
        private DOFade script { get => target as DOFade; }

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
            EditorGUILayout.BeginHorizontal();
            script.fadeSource = (DOFade.FadeSource)EditorGUILayout.EnumPopup("Source", script.fadeSource);
            if (script.fadeSource == DOFade.FadeSource.Material)
            {
                script.materialIndex = EditorGUILayout.IntField("Index", script.materialIndex);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            Undo.RecordObject(this, "ChangedStartValue");
            switch (script.fadeSource)
            {
                case DOFade.FadeSource.Image:
                    script.startValue = script.GetComponent<Image>().color.a;
                    break;
                case DOFade.FadeSource.CanvasGroup:
                    script.startValue = script.GetComponent<CanvasGroup>().alpha;
                    break;
                case DOFade.FadeSource.SpriteRenderer:
                    script.startValue = script.GetComponent<SpriteRenderer>().color.a;
                    break;
                case DOFade.FadeSource.Material:
                    script.startValue = script.GetComponent<Renderer>().materials[script.materialIndex].color.a;
                    break;
            }
            EditorUtility.SetDirty(this);
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            switch (script.fadeSource)
            {
                case DOFade.FadeSource.Image:
                    Color color = script.GetComponent<Image>().color;
                    color.a = script.startValue;
                    script.GetComponent<Image>().color = color;
                    break;
                case DOFade.FadeSource.CanvasGroup:
                    script.GetComponent<CanvasGroup>().alpha = script.startValue;
                    break;
                case DOFade.FadeSource.SpriteRenderer:
                    Color color2 = script.GetComponent<SpriteRenderer>().color;
                    color2.a = script.startValue;
                    script.GetComponent<SpriteRenderer>().color = color2;
                    break;
                case DOFade.FadeSource.Material:
                    Color color3 = script.GetComponent<Renderer>().materials[script.materialIndex].color;
                    color3.a = script.startValue;
                    script.GetComponent<Renderer>().materials[script.materialIndex].color = color3;
                    break;
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            Undo.RecordObject(this, "ChangedEndValue");
            switch (script.fadeSource)
            {
                case DOFade.FadeSource.Image:
                    script.endValue = script.GetComponent<Image>().color.a;
                    break;
                case DOFade.FadeSource.CanvasGroup:
                    script.endValue = script.GetComponent<CanvasGroup>().alpha;
                    break;
                case DOFade.FadeSource.SpriteRenderer:
                    script.endValue = script.GetComponent<SpriteRenderer>().color.a;
                    break;
                case DOFade.FadeSource.Material:
                    script.endValue = script.GetComponent<Renderer>().materials[script.materialIndex].color.a;
                    break;
            }
            EditorUtility.SetDirty(script);

        }
        void ApplyEndValue()
        {
            Undo.RecordObject(script.transform, "ApplyedEndValue");
            switch (script.fadeSource)
            {
                case DOFade.FadeSource.Image:
                    Color color = script.GetComponent<Image>().color;
                    color.a = script.endValue;
                    script.GetComponent<Image>().color = color;
                    break;
                case DOFade.FadeSource.CanvasGroup:
                    script.GetComponent<CanvasGroup>().alpha = script.endValue;
                    break;
                case DOFade.FadeSource.SpriteRenderer:
                    Color color2 = script.GetComponent<SpriteRenderer>().color;
                    color2.a = script.endValue;
                    script.GetComponent<SpriteRenderer>().color = color2;
                    break;
                case DOFade.FadeSource.Material:
                    Color color3 = script.GetComponent<Renderer>().materials[script.materialIndex].color;
                    color3.a = script.endValue;
                    script.GetComponent<Renderer>().materials[script.materialIndex].color = color3;
                    break;
            }
            EditorUtility.SetDirty(script);

        }
    }
}
