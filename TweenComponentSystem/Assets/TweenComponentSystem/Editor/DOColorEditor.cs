using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOColor))]
    public class DOColorEditor : Editor
    {
        private DOColor script { get => target as DOColor; }

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
            script.startColor = EditorGUILayout.ColorField("Start Color", script.startColor);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyStartValue },
                new DOBase.ContextItem { name = "Record", function = RecordStart });

            script.endColor = EditorGUILayout.ColorField("End Color", script.endColor);
            script.ContextMenu(new DOBase.ContextItem { name = "Apply", function = ApplyEndValue },
                new DOBase.ContextItem { name = "Record", function = RecordEnd });
            EditorGUILayout.BeginHorizontal();
            script.colorSource = (DOColor.ColorSource)EditorGUILayout.EnumPopup("Source", script.colorSource);
            if (script.colorSource == DOColor.ColorSource.Material)
            {
                script.materialIndex = EditorGUILayout.IntField("Index", script.materialIndex);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void RecordStart()
        {
            Undo.RecordObject(this, "ChangedStartValue");
            switch (script.colorSource)
            {
                case DOColor.ColorSource.Image:
                    script.startColor = script.GetComponent<Image>().color;
                    break;
                case DOColor.ColorSource.SpriteRenderer:
                    script.startColor = script.GetComponent<SpriteRenderer>().color;
                    break;
                case DOColor.ColorSource.Material:
                    script.startColor = script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color;
                    break;
            }
            EditorUtility.SetDirty(this);
        }
        void ApplyStartValue()
        {
            Undo.RecordObject(script.transform, "ApplyedStartValue");
            switch (script.colorSource)
            {
                case DOColor.ColorSource.Image:
                    Color color = script.GetComponent<Image>().color;
                    color = script.startColor;
                    script.GetComponent<Image>().color = color;
                    break;
                case DOColor.ColorSource.SpriteRenderer:
                    Color color2 = script.GetComponent<SpriteRenderer>().color;
                    color2 = script.startColor;
                    script.GetComponent<SpriteRenderer>().color = color2;
                    break;
                case DOColor.ColorSource.Material:
                    Color color3 = script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color;
                    color3 = script.startColor;
                    script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color = color3;
                    break;
            }
            EditorUtility.SetDirty(script);
        }
        void RecordEnd()
        {
            Undo.RecordObject(this, "ChangedEndValue");
            switch (script.colorSource)
            {
                case DOColor.ColorSource.Image:
                    script.endColor = script.GetComponent<Image>().color;
                    break;
                case DOColor.ColorSource.SpriteRenderer:
                    script.endColor = script.GetComponent<SpriteRenderer>().color;
                    break;
                case DOColor.ColorSource.Material:
                    script.endColor = script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color;
                    break;
            }
            EditorUtility.SetDirty(script);

        }
        void ApplyEndValue()
        {
            Undo.RecordObject(script.transform, "ApplyedEndValue");
            switch (script.colorSource)
            {
                case DOColor.ColorSource.Image:
                    Color color = script.GetComponent<Image>().color;
                    color = script.endColor;
                    script.GetComponent<Image>().color = color;
                    break;
                case DOColor.ColorSource.SpriteRenderer:
                    Color color2 = script.GetComponent<SpriteRenderer>().color;
                    color2 = script.endColor;
                    script.GetComponent<SpriteRenderer>().color = color2;
                    break;
                case DOColor.ColorSource.Material:
                    Color color3 = script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color;
                    color3 = script.endColor;
                    script.GetComponent<Renderer>().sharedMaterials[script.materialIndex].color = color3;
                    break;
            }
            EditorUtility.SetDirty(script);

        }
    }
}
