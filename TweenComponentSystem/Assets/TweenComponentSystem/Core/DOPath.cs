using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace TweenComponentSystem
{
    public class DOPath : DOBase
    {
        public List<Transform> transforms;

        public PathType pathType = PathType.Linear;
        public PathMode pathMode = PathMode.Full3D;
        public LookAt lookAt = LookAt.LookAhead;
        public Transform lookAtTransform;
        public Vector3 lookAtPosition;
        public float lookAhead;
        public int resolution = 10;
        public Color gizmoColor = Color.white;
        public bool snapOnStart;
        public bool livePositions;

        public bool local;

        private Vector3[] positions;

        internal override void VirtualEnable()
        {
            base.VirtualEnable();

            TransformsToPositions();
        }
        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (livePositions)
                {
                    TransformsToPositions();
                }
                if (positions != null && positions.Length > 1)
                {
                    if (snapOnStart)
                    {
                        if (local)
                            c_Transform.localPosition = transforms[0].localPosition;
                        else
                            c_Transform.position = transforms[0].position;
                    }
                    switch (lookAt)
                    {
                        case LookAt.LookAhead:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAhead).OnComplete(() => doComplete.Invoke());
                            break;
                        case LookAt.LookAtTransform:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAtTransform).OnComplete(() => doComplete.Invoke());
                            break;
                        case LookAt.LookAtPosition:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAtPosition).OnComplete(() => doComplete.Invoke());
                            break;
                    }
                }
                base.DO();
            }
            else
            {
                if (transforms != null && transforms.Count > 0)
                {
#if UNITY_EDITOR
                    Undo.RecordObject(transform, "Changed transform");
#endif
                    if (local)
                    {
                        transform.localPosition = transforms[transforms.Count - 1].localPosition;
                    }
                    else
                    {
                        transform.position = transforms[transforms.Count - 1].position;
                    }
#if UNITY_EDITOR
                    EditorUtility.SetDirty(transform);
#endif
                }
            }
        }
        public override void DORevert()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (livePositions)
                {
                    TransformsToPositions();
                }
                if (positions != null && positions.Length > 1)
                {
                    if (snapOnStart)
                    {
                        if (local)
                            c_Transform.localPosition = transforms[transforms.Count - 1].localPosition;
                        else
                            c_Transform.position = transforms[transforms.Count - 1].position;
                    }
                    switch (lookAt)
                    {
                        case LookAt.LookAhead:
                            tween = c_Transform.DOPath(positions.Reverse().ToArray(), revertDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(twoEase ? revertEase : doEase).SetDelay(revertDelay).SetLookAt(lookAhead).OnComplete(() => doRevertComplete.Invoke());
                            break;
                        case LookAt.LookAtTransform:
                            tween = c_Transform.DOPath(positions.Reverse().ToArray(), revertDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(twoEase ? revertEase : doEase).SetDelay(revertDelay).SetLookAt(lookAtTransform).OnComplete(() => doRevertComplete.Invoke());
                            break;
                        case LookAt.LookAtPosition:
                            tween = c_Transform.DOPath(positions.Reverse().ToArray(), revertDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(twoEase ? revertEase : doEase).SetDelay(revertDelay).SetLookAt(lookAtPosition).OnComplete(() => doRevertComplete.Invoke());
                            break;
                    }
                }
                base.DORevert();
            }
            else
            {
                if (transforms != null && transforms.Count > 0)
                {
#if UNITY_EDITOR
                    Undo.RegisterCompleteObjectUndo(transform, "Changed transform");
#endif
                    if (local)
                    {
                        transform.localPosition = transforms[0].localPosition;
                    }
                    else
                    {
                        transform.position = transforms[0].position;
                    }
#if UNITY_EDITOR
                    EditorUtility.SetDirty(transform);
#endif
                }
            }
        }
        public override void ResetDO()
        {
            transform.DOKill(true);
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
            if (transforms != null && transforms.Count > 1)
            {
                if (local)
                {
                    if (transforms[0] != null)
                        transform.localPosition = transforms[0].localPosition;
                }
                else
                {
                    if (transforms[0] != null)
                        transform.position = transforms[0].position;
                }
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(transform);
#endif
        }
        public override void DOLoop()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (livePositions)
                {
                    TransformsToPositions();
                }
                if (positions != null && positions.Length > 1)
                {
                    if (snapOnStart)
                    {
                        if (local)
                            c_Transform.localPosition = transforms[0].localPosition;
                        else
                            c_Transform.position = transforms[0].position;
                    }
                    switch (lookAt)
                    {
                        case LookAt.LookAhead:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAhead).SetLoops(-1, loopType);
                            break;
                        case LookAt.LookAtTransform:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAtTransform).SetLoops(-1, loopType);
                            break;
                        case LookAt.LookAtPosition:
                            tween = c_Transform.DOPath(positions, doDuration, pathType, pathMode, resolution, gizmoColor).
                        SetEase(doEase).SetDelay(doDelay).SetLookAt(lookAtPosition).SetLoops(-1, loopType);
                            break;
                    }
                }
            }
        }
        public override void Kill()
        {
            if (DOTween.IsTweening(transform))
                transform.DOKill();
        }
        private void TransformsToPositions()
        {
            if (transforms != null && transforms.Count > 1)
            {
                List<Vector3> poses = new List<Vector3>();
                for (int i = 0; i < transforms.Count; i++)
                {
                    if (transforms[i] != null)
                    {
                        if (!local)
                            poses.Add(transforms[i].position);
                        else
                            poses.Add(transforms[i].localPosition);
                    }
                }
                positions = poses.ToArray();
            }
        }
        private void OnDrawGizmos()
        {
            if (transforms != null && transforms.Count > 0)
            {
                Gizmos.color = gizmoColor;
                for (int i = 0; i < transforms.Count; i++)
                {
                    Gizmos.DrawSphere(transforms[i].position, 0.3F);
                    if (i + 1 < transforms.Count)
                        Gizmos.DrawLine(transforms[i].position, transforms[i + 1].position);
                }
            }
        }
        public enum LookAt
        {
            LookAhead,
            LookAtTransform,
            LookAtPosition
        }
    }
}