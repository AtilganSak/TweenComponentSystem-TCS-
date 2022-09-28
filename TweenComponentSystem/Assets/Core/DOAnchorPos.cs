using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class DOAnchorPos : DOBase
    {
        public bool useStartTarget;
        public RectTransform startTarget;

        public bool useEndTarget;
        public RectTransform endTarget;

        public Vector2 startValue;
        public Vector2 endValue = Vector2.one;

        RectTransform c_TransformRect;

        internal override void VirtualEnable()
        {
            c_TransformRect = GetComponent<RectTransform>();
        }
        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_TransformRect))
                        c_TransformRect.DOKill();
                }
                if (useEndTarget)
                    tween = c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                else
                    tween = c_TransformRect.DOAnchorPos(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());

                base.DO();
            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = endValue;
            }
        }
        public override void DORevert()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_TransformRect))
                        c_TransformRect.DOKill();
                }
                if (useStartTarget)
                    tween = c_TransformRect.DOAnchorPos(startTarget.anchoredPosition, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doComplete.Invoke());
                else
                    tween = c_TransformRect.DOAnchorPos(startValue, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());

                base.DORevert();
            }
            else
            {
                GetComponent<RectTransform>().anchoredPosition = startValue;
            }
        }
        public override void ResetDO()
        {
#if UNITY_EDITOR
            Undo.RecordObject(gameObject, name + "Changed transform");
#endif
            GetComponent<RectTransform>().DOKill();
            if (useStartTarget)
                GetComponent<RectTransform>().anchoredPosition = startTarget.anchoredPosition;
            else
                GetComponent<RectTransform>().anchoredPosition = startValue;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
        public override void DOLoop()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_TransformRect))
                        c_TransformRect.DOKill();
                }
                if (useEndTarget)
                    tween = c_TransformRect.DOAnchorPos(endTarget.anchoredPosition, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                else
                    tween = c_TransformRect.DOAnchorPos(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
            }
            else
            {
                if (useEndTarget)
                    GetComponent<RectTransform>().anchoredPosition = endTarget.anchoredPosition;
                else
                    GetComponent<RectTransform>().anchoredPosition = endValue;
            }
        }
        public override void Kill()
        {
            if (DOTween.IsTweening(c_TransformRect))
                c_TransformRect.DOKill();
        }
    }
}
