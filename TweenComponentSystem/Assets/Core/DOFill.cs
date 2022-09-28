using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TweenComponentSystem
{
    [RequireComponent(typeof(Image))]
    public class DOFill : DOBase
    {
        public float startValue = 0;
        public float endValue = 1;

        Image sourceImage;

        internal override void VirtualEnable()
        {
            sourceImage = GetComponent<Image>();
        }
        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                }
                tween = sourceImage.DOFillAmount(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                base.DO();
            }
            else
            {
                sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                Undo.RecordObject(sourceImage, "Changed Image Fill Amount");
#endif
                sourceImage.fillAmount = endValue;
#if UNITY_EDITOR
                EditorUtility.SetDirty(sourceImage);
#endif
            }
        }
        public override void DORevert()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                }
                tween = sourceImage.DOFillAmount(startValue, twoDuration ? revertDuration : doDuration).SetDelay(twoDelay ? revertDelay : doDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                base.DORevert();
            }
            else
            {
                sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
                Undo.RecordObject(sourceImage, "Changed Image Fill Amount");
#endif
                sourceImage.fillAmount = startValue;
#if UNITY_EDITOR
                EditorUtility.SetDirty(sourceImage);
#endif
            }
        }
        public override void ResetDO()
        {
            if (Application.isPlaying)
                sourceImage.DOKill(true);
            else
                sourceImage = GetComponent<Image>();
#if UNITY_EDITOR
            Undo.RecordObject(sourceImage, "Changed Image Fill Amount");
#endif
            sourceImage.fillAmount = startValue;
#if UNITY_EDITOR
            EditorUtility.SetDirty(sourceImage);
#endif
        }
        public override void DOLoop()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(sourceImage))
                        sourceImage.DOKill(true);
                }
                tween = sourceImage.DOFillAmount(endValue, doDuration).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
            }
        }
        public override void Kill()
        {
            if (Application.isPlaying)
            {
                if (DOTween.IsTweening(sourceImage))
                    sourceImage.DOKill();
            }
        }
    }
}
