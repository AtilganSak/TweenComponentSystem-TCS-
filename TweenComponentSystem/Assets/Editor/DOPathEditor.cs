using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    [CustomEditor(typeof(DOPath))]
    public class DOPathEditor : Editor
    {
        private DOPath script { get => target as DOPath; }

        private Vector2 scrollPos;
        private int currentPickerWindow;

        private void OnEnable()
        {
            script.Enable();
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            script.DrawParameters();
            script.DrawToggles();
            DrawPoints();
            DrawValues();
            script.DrawEvents();
            script.DrawTweenKey();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(script);
            }
            script.DrawButtons();
        }
        private void DrawPoints()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Points", script.headerStyle, GUILayout.Width(60));
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Drag and Drop Transforms here!", EditorStyles.centeredGreyMiniLabel);
            Rect dragAndDropRect = GUILayoutUtility.GetLastRect();
            if (GUILayout.Button("", GUI.skin.GetStyle("IN ObjectField"), GUILayout.Width(20)))
            {
                currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
                EditorGUIUtility.ShowObjectPicker<Transform>(null, true, "", currentPickerWindow);
            }
            if (Event.current.commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID() == currentPickerWindow)
            {
                if (script.transforms == null)
                    script.transforms = new System.Collections.Generic.List<Transform>();
                script.transforms.Add(((GameObject)EditorGUIUtility.GetObjectPickerObject()).transform);
                currentPickerWindow = -1;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();
            if (dragAndDropRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.DragUpdated)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.DragPerform)
                {
                    if (script.transforms == null)
                        script.transforms = new System.Collections.Generic.List<Transform>();
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                    {
                        script.transforms.Add(((GameObject)DragAndDrop.objectReferences[i]).transform);
                    }
                    Event.current.Use();
                }
            }
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(150));
            if (script.transforms != null && script.transforms.Count > 0)
            {
                for (int i = 0; i < script.transforms.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(i + " - ", script.headerStyle, GUILayout.Width(20));
                    script.transforms[i] = (Transform)EditorGUILayout.ObjectField(script.transforms[i].name,
                        script.transforms[i], typeof(Transform), true);
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        script.transforms.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField("No Points!", EditorStyles.centeredGreyMiniLabel);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        private void DrawValues()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Values", script.headerStyle);
            EditorGUILayout.BeginHorizontal();
            script.pathType = (PathType)EditorGUILayout.EnumPopup("Path Type", script.pathType);
            script.pathMode = (PathMode)EditorGUILayout.EnumPopup("Path Mode", script.pathMode);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            script.resolution = EditorGUILayout.IntField("Resolution", script.resolution);
            script.gizmoColor = EditorGUILayout.ColorField("Gizmo Color", script.gizmoColor);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            script.lookAt = (DOPath.LookAt)EditorGUILayout.EnumPopup("Look At", script.lookAt);
            switch (script.lookAt)
            {
                case DOPath.LookAt.LookAhead:
                    EditorGUIUtility.labelWidth = 50;
                    script.lookAhead = EditorGUILayout.Slider(new GUIContent("Ahead", "The percentage of lookAhead to use (0 to 1)"), script.lookAhead, 0, 1);
                    EditorGUIUtility.labelWidth = 100;
                    break;
                case DOPath.LookAt.LookAtTransform:
                    script.lookAtTransform = (Transform)EditorGUILayout.ObjectField(new GUIContent("Transform", "The transform to look at"), script.lookAtTransform, typeof(Transform), true);
                    break;
                case DOPath.LookAt.LookAtPosition:
                    EditorGUIUtility.labelWidth = 50;
                    script.lookAtPosition = EditorGUILayout.Vector3Field(new GUIContent("Position", " The position to look at"), script.lookAtPosition);
                    EditorGUIUtility.labelWidth = 100;
                    break;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            script.local = EditorGUILayout.Toggle("Local", script.local);
            script.snapOnStart = EditorGUILayout.Toggle("Snap On Start", script.snapOnStart);
            script.livePositions = EditorGUILayout.Toggle(new GUIContent("Live Positions", "Convert transforms to positions before every action"), script.livePositions);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}
