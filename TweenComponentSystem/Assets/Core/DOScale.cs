using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace TweenComponentSystem
{
    public class DOScale : DOBase
    {
        public Vector3 startValue = Vector3.one;

        public Vector3 endValue = Vector3.one;

        public bool lossy;

        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                tween = c_Transform.DOScale(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());

                base.DO();
            }
            else
            {
                transform.localScale = endValue;
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
                tween = c_Transform.DOScale(startValue, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());

                base.DORevert();
            }
            else
            {
                transform.localScale = startValue;
            }
        }
        public override void ResetDO()
        {
#if UNITY_EDITOR
            Undo.RecordObject(gameObject, name + "Changed transform");
#endif
            transform.DOKill(true);
            transform.localScale = startValue;

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
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                tween = c_Transform.DOScale(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
            }
            else
            {
                transform.localScale = endValue;
            }
        }
        public override void Kill()
        {
            if (DOTween.IsTweening(transform))
                transform.DOKill();
        }
    }
}
