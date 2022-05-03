using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TweenComponentSystem
{
    public class DOBase : MonoBehaviour
    {
        public float doDuration = 1;
        public float revertDuration = 1;
        public float doDelay;
        public float revertDelay;
        public Ease doEase;
        public Ease revertEase;
        public LoopType loopType;
        public bool resetOnEnable;
        public bool doOnStart;
        public bool doRevertOnStart;
        public bool loopOnStart;
        public bool allowTwin;

        public bool twoDuration;
        public bool twoDelay;
        public bool twoEase;

        public UnityEvent doComplete;
        public UnityEvent doRevertComplete;

        public TweenKey tweenKey;

        internal bool doRun;
        internal bool doRevertRun;

        private float percentageDuration;

        private WaitForSeconds wait;

        private Transform tr;
        internal Transform c_Transform
        {
            get
            {
                if (tr == null)
                    tr = transform;
                return tr;
            }
        }
        internal Tween tween;

        [System.Serializable]
        public struct TweenKey
        {
            [Range(1, 99)]
            public float percentage;
            public UnityEvent doReachedEvent;
            public UnityEvent doRevertReachedEvent;
            [HideInInspector] public bool ok;
        }

        private void OnEnable()
        {
            if (tweenKey.percentage < 1)
                tweenKey.percentage = 1;

            percentageDuration = doDuration * (tweenKey.percentage / 100);

            wait = new WaitForSeconds(percentageDuration);

            VirtualEnable();

            if (resetOnEnable)
                ResetDO();
        }
        private void OnDestroy()
        {
            ResetDO();
        }
        private void Start()
        {
            if (doOnStart)
                DO();

            if (loopOnStart)
                DOLoop();

            VirtualStart();
        }
        private IEnumerator DOTweenKey()
        {
            while (!tweenKey.ok)
            {
                yield return wait;
                if (tween != null)
                {
                    if (!tweenKey.ok)
                    {
                        tweenKey.ok = true;
                        if (doRun)
                            tweenKey.doReachedEvent.Invoke();
                        else if (doRevertRun)
                            tweenKey.doRevertReachedEvent.Invoke();
                        doRun = false;
                        doRevertRun = false;
                    }
                }
            }
        }
        private void Reset()
        {
            tweenKey.percentage = 1;
        }
        internal virtual void VirtualStart() { }
        internal virtual void VirtualEnable() { }

        #region Capsulate
        public void SetDODuration(float value) => doDuration = value;
        public void SetRevertDuration(float value) => revertDuration = value;
        public void SetDODelay(float value) => doDelay = value;
        public void SetRevertDelay(float value) => revertDelay = value;
        #endregion

        public bool IsTweening()
        {
            if (tween != null && tween.IsActive())
            {
                return tween.IsPlaying();
            }
            else
            {
                return false;
            }
        }
        public virtual void DO()
        {
            tweenKey.ok = false;
            doRun = true;
            //if(tweenKey.doReachedEvent.GetPersistentEventCount() > 0)
            StartCoroutine(DOTweenKey());
        }
        public virtual void DOLoop() { }
        public virtual void DORevert()
        {
            tweenKey.ok = false;
            doRevertRun = true;
            //if (tweenKey.doRevertReachedEvent.GetPersistentEventCount() > 0)
            StartCoroutine(DOTweenKey());
        }
        public virtual void ResetDO() { }
        public virtual void Kill() { }
        public virtual void Pause() 
        {
            if (tween == null) return;

            if (tween.IsPlaying())
            {
                c_Transform.DOPause();
            }
        }
        public virtual void Play() 
        {
            if (tween == null) return;

            if (!tween.IsPlaying())
            {
                c_Transform.DOPlay();
            }
        }

#if UNITY_EDITOR
        public GUIStyle headerStyle;
        private bool tweenKeyFoldout;
        private bool eventsFoldout;

        private SerializedObject so;
        private SerializedProperty tweenKeyDOCompleteEventProp;
        private SerializedProperty tweenKeyDORevertCompleteEventProp;
        private SerializedProperty doCompleteEventProp;
        private SerializedProperty doRevertCompleteEventProp;

        public void Enable()
        {
            so = new SerializedObject(this);
            tweenKeyDOCompleteEventProp = so.FindProperty("tweenKey").FindPropertyRelative("doReachedEvent");
            tweenKeyDORevertCompleteEventProp = so.FindProperty("tweenKey").FindPropertyRelative("doRevertReachedEvent");
            doCompleteEventProp = so.FindProperty("doComplete");
            doRevertCompleteEventProp = so.FindProperty("doRevertComplete");

            headerStyle = new GUIStyle();
            headerStyle.alignment = TextAnchor.MiddleLeft;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.white;
        }
        public void DrawParameters()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Parameters", headerStyle);
            Durations();
            Delays();
            Eases();
            LoopType();
            EditorGUILayout.EndVertical();
        }
        private void Durations()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100;
            doDuration = EditorGUILayout.FloatField(twoDuration ? "Do Duration" : "Duration", doDuration);
            if (twoDuration)
                revertDuration = EditorGUILayout.FloatField("Revert Duration", revertDuration);
            twoDuration = EditorGUILayout.Toggle(new GUIContent("", "Two Constant"), twoDuration, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
        }
        private void Delays()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100;
            doDelay = EditorGUILayout.FloatField(twoDelay ? "Do Delay" : "Delay", doDelay);
            if (twoDelay)
                revertDelay = EditorGUILayout.FloatField("Revert Delay", revertDelay);
            twoDelay = EditorGUILayout.Toggle(new GUIContent("", "Two Constant"), twoDelay, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
        }
        private void Eases()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100;
            doEase = (DG.Tweening.Ease)EditorGUILayout.EnumPopup(twoEase ? "Do Ease" : "Ease", doEase);
            if (twoEase)
                revertEase = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Revert Ease", revertEase);
            twoEase = EditorGUILayout.Toggle(new GUIContent("", "Two Constant"), twoEase, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();
        }
        private void LoopType()
        {
            loopType = (DG.Tweening.LoopType)EditorGUILayout.EnumPopup("Loop Type", loopType);
        }
        public void DrawToggles()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Toggles", headerStyle);
            EditorGUILayout.BeginHorizontal();
            resetOnEnable = EditorGUILayout.Toggle("Reset Enable", resetOnEnable);
            loopOnStart = EditorGUILayout.Toggle("Loop Start", loopOnStart);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            doOnStart = EditorGUILayout.Toggle("DO Start", doOnStart);
            doRevertOnStart = EditorGUILayout.Toggle("Revert Start", doRevertOnStart);
            EditorGUILayout.EndHorizontal();
            allowTwin = EditorGUILayout.Toggle("Allow Twin", allowTwin);
            EditorGUILayout.EndVertical();
        }
        public void DrawTweenKey()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUI.indentLevel++;
            tweenKeyFoldout = EditorGUILayout.Foldout(tweenKeyFoldout, "Tween Key", true, headerStyle);
            EditorGUI.indentLevel--;
            if (tweenKeyFoldout)
            {
                tweenKey.percentage = EditorGUILayout.Slider("Percentage", tweenKey.percentage, 1, 99);
                so.Update();
                EditorGUILayout.PropertyField(tweenKeyDOCompleteEventProp);
                EditorGUILayout.PropertyField(tweenKeyDORevertCompleteEventProp);
                so.ApplyModifiedProperties();
            }
            EditorGUILayout.EndVertical();
        }
        public void DrawEvents()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUI.indentLevel++;
            eventsFoldout = EditorGUILayout.Foldout(eventsFoldout, "Events", true, headerStyle);
            EditorGUI.indentLevel--;
            if (eventsFoldout)
            {
                so.Update();
                EditorGUILayout.PropertyField(doCompleteEventProp);
                EditorGUILayout.PropertyField(doRevertCompleteEventProp);
                so.ApplyModifiedProperties();
            }
            EditorGUILayout.EndVertical();
        }
        public void DrawButtons()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Actions", headerStyle);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("DO"))
            {
                DO();
            }
            if (GUILayout.Button("REVERT"))
            {
                DORevert();
            }
            if (GUILayout.Button("LOOP"))
            {
                DOLoop();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("RESET"))
            {
                ResetDO();
            }
            if (GUILayout.Button("KILL"))
            {
                Kill();
            }
            bool isPLaying = tween == null ? false : tween.IsPlaying();
            if (GUILayout.Button(isPLaying ? "PAUSE" : "PLAY"))
            {
                if (isPLaying)
                {
                    Pause();
                }
                else
                {
                    Play();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        public void ContextMenu(params ContextItem[] items)
        {
            Rect clickArea = GUILayoutUtility.GetLastRect();
            Event current = Event.current;
            if (clickArea.Contains(current.mousePosition) && current.type == EventType.ContextClick)
            {
                GenericMenu menu = new GenericMenu();
                if (items != null && items.Length > 0)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        menu.AddItem(new GUIContent(items[i].name), false, items[i].function);
                    }
                }
                menu.ShowAsContext();
                current.Use();
            }
        }
        [System.Serializable]
        public struct ContextItem
        {
            public string name;
            public GenericMenu.MenuFunction function;
        }
#endif
    }
}