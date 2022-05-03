using DG.Tweening;
using UnityEngine;
using UnityEditor;

namespace TweenComponentSystem
{
    public class DOMove : DOBase
    {
        public bool useStartTarget;
        public bool useEndTarget;

        public Transform startTarget;
        public Transform endTarget;

        public Vector3 startValue;
        public Vector3 endValue = Vector3.one;

        public bool local;

        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (useEndTarget)
                {
                    if (!local)
                        tween = c_Transform.DOMove(endTarget.position, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                    else
                        tween = c_Transform.DOLocalMove(endTarget.localPosition, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                }
                else
                {
                    if (!local)
                        tween = c_Transform.DOMove(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                    else
                        tween = c_Transform.DOLocalMove(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                }
                base.DO();
            }
            else
            {
#if UNITY_EDITOR
                Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
                if (useEndTarget)
                {
                    if (!local)
                        transform.position = endTarget.position;
                    else
                        transform.localPosition = endTarget.localPosition;
                }
                else
                {
                    if (!local)
                        transform.position = endValue;
                    else
                        transform.localPosition = endValue;
                }

#if UNITY_EDITOR
                EditorUtility.SetDirty(transform);
#endif
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
                if (useStartTarget)
                {
                    if (!local)
                        tween = c_Transform.DOMove(startTarget.position, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                    else
                        tween = c_Transform.DOLocalMove(startTarget.localPosition, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                }
                else
                {
                    if (!local)
                        tween = c_Transform.DOMove(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                    else
                        tween = c_Transform.DOLocalMove(startValue, doDuration).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                }
                base.DORevert();
            }
            else
            {
#if UNITY_EDITOR
                Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
                if (useStartTarget)
                {
                    if (!local)
                        transform.position = startTarget.position;
                    else
                        transform.localPosition = startTarget.localPosition;
                }
                else
                {
                    if (!local)
                        transform.position = startValue;
                    else
                        transform.localPosition = startValue;
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(transform);
#endif
            }
        }
        public override void ResetDO()
        {
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
#endif
            transform.DOKill(true);
            if (useStartTarget)
            {
                if (!local)
                    transform.position = startTarget.position;
                else
                    transform.localPosition = startTarget.localPosition;
            }
            else
            {
                if (!local)
                    transform.position = startValue;
                else
                    transform.localPosition = startValue;
            }
#if UNITY_EDITOR
            Undo.RegisterCompleteObjectUndo(transform, "Changed Transform Poisition");
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
                if (useEndTarget)
                {
                    if (!local)
                        tween = c_Transform.DOMove(endTarget.position, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    else
                        tween = c_Transform.DOLocalMove(endTarget.localPosition, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                }
                else
                {
                    if (!local)
                        tween = c_Transform.DOMove(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                    else
                        tween = c_Transform.DOLocalMove(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                }
            }
            else
            {
                if (useEndTarget)
                {
                    if (!local)
                        transform.position = endTarget.position;
                    else
                        transform.localPosition = endTarget.localPosition;
                }
                else
                {
                    if (!local)
                        transform.position = endValue;
                    else
                        transform.localPosition = endValue;
                }
            }
        }
        public override void Kill()
        {
            if (DOTween.IsTweening(transform))
                transform.DOKill(true);
        }
    }
}
